﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HumanityService.DataContracts.CompositeDesignPattern;
using HumanityService.DataContracts.Requests;
using HumanityService.DataContracts.Results;
using HumanityService.Services.Interfaces;
using HumanityService.Stores.Interfaces;

namespace HumanityService.Services
{
    public class TransactionService : ITransactionService
    {

        private readonly ITransactionStore _transactionStore;
        private readonly INotificationService _notificationService;
        private readonly IMatchingService _matchingService;

        public TransactionService(ITransactionStore transactionStore, IMatchingService matchingService)
        {
            _transactionStore = transactionStore;
            _matchingService = matchingService;
        }

        public async Task<Campaign> MatchCampaign(MatchCampaignRequest request)
        {
            var campaign = await _matchingService.MatchUserToCampaign(request);
            return campaign;
        }

        public async Task<DeliveryDemand> MatchDeliveryDemand(MatchDeliveryDemandRequest request)
        {
            var deliveryDemand = await _matchingService.MatchUserToDeliveryDemand(request);
            return deliveryDemand;
        }

        public async Task AnswerCampaign(string campaignId, AnswerCampaignRequest request)
        {
            Campaign campaign = await BuildCampaign(campaignId);
            await campaign.AnswerCampaign(request);
        }

        public async Task AnswerDeliveryDemand(string deliveryDemandId, AnswerDeliveryDemandRequest request)
        {
            var deliveryDemand = await _transactionStore.GetDeliveryDemand(deliveryDemandId);
            Process process = await BuildProcess(deliveryDemand.ProcessId);
            await process.AnswerDeliveryDemand(deliveryDemandId, request);
        }

        public async Task<bool> ValidateDelivery(ValidateDeliveryRequest request) 
        {
            if(request.ValidationType == "Pickup")
            {
                var contribution = await _transactionStore.GetContribution(request.ContributionId);
                Process process = await BuildProcess(contribution.ProcessId);
                if (process.DeliveryCode == request.DeliveryCode)
                {
                    await process.ValidateDelivery("Pickup");
                    return true;
                }
                else return false;
            }
            else
            {
                var getProcessesResult = await _transactionStore.GetProcesses(request.CampaignId);
                Process process = getProcessesResult.Processes.Find(request => request.DeliveryCode == request.DeliveryCode);
                if (process != null)
                {
                    process = await BuildProcess(process.Id);
                    await process.ValidateDelivery("Destination");
                    var campaign = await _transactionStore.GetCampaign(process.CampaignId);
                    campaign.CompletedCount++;
                    await _transactionStore.UpdateCampaign(campaign);
                    return true;
                }
                else return false;
            }
        }

        public Task ApproveContribution(string contributionId)
        {
            throw new NotImplementedException();
            //update contribution
            //Used with volunteering contributions
        }

        public async Task CreateCampaign(CreateCampaignRequest request)
        {
            Campaign campaign = new Campaign(request);
            campaign.SetStore(_transactionStore);
            await campaign.Save();
        }

        public async Task CancelCampaign(string campaignId)
        {
            Campaign campaign = await BuildCampaign(campaignId);
            await campaign.Cancel();
        }

        public async Task CancelContribution(string contributionId)
        {
            var contribution = await _transactionStore.GetContribution(contributionId);
            var process = await BuildProcess(contribution.ProcessId);

            if (contribution.Type == "Delivery")
            {
                await process.CancelDeliveryContribution();
            }
            else
            {
                await process.Cancel();
            }
        }

        public async Task EditCampaign(string campaignId, EditCampaignRequest request)
        {
            var campaign = await _transactionStore.GetCampaign(campaignId);
            campaign.Name = request.CampaignName;
            campaign.Type = request.Type;
            campaign.Category = request.Category;
            campaign.Target = request.Target;
            campaign.Description = request.Description;
            await _transactionStore.UpdateCampaign(campaign);
        }

        public async Task<Campaign> GetCampaign(string campaignId)
        {
            var campaign = await _transactionStore.GetCampaign(campaignId);
            return campaign;
        }

        public async Task<GetCampaignsResult> GetCampaigns(GetCampaignsRequest request)
        {
            var getCampaignsResult = await _transactionStore.GetCampaigns(request);
            return getCampaignsResult;
        }

        public async Task<Contribution> GetContribution(string contributionId)
        {
            var contribution = await _transactionStore.GetContribution(contributionId);
            return contribution;
        }

        public async Task<GetContributionsResult> GetContributions(GetContributionsRequest request)
        {
            var getContributionsResult = await _transactionStore.GetContributions(request);
            return getContributionsResult;
        }

        public async Task<DeliveryDemand> GetDeliveryDemand(string deliveryDemandId)
        {
            var deliveryDemand = await _transactionStore.GetDeliveryDemand(deliveryDemandId);
            return deliveryDemand;
        }

        public async Task<GetDeliveryDemandsResult> GetDeliveryDemands(GetDeliveryDemandsRequest request)
        {
            var getDeliveryDemandsResult = await _transactionStore.GetDeliveryDemands(request);
            return getDeliveryDemandsResult;
        }

        public async Task<Process> GetProcess(string processId)
        {
            var process = await _transactionStore.GetProcess(processId);
            return process;
        }

        public async Task<GetProcessesResult> GetProcesses(string campaignId)
        {
            var getProcessesResult = await _transactionStore.GetProcesses(campaignId);
            return getProcessesResult;
        }

        private async Task<Process> BuildProcess(string processId)
        {
            Process process = await _transactionStore.GetProcess(processId);
            var getContributionsRequest = new GetContributionsRequest
            {
                ProcessId = processId
            };
            var contributions = await _transactionStore.GetContributions(getContributionsRequest);
            var donorContribution = contributions.Contributions.Find(contribution => contribution.Type != "Delivery");
            var getDeliveryDemands = new GetDeliveryDemandsRequest
            {
                ProcessId = processId
            };
            var deliveryDemands = await _transactionStore.GetDeliveryDemands(getDeliveryDemands);
            var deliveryDemand = deliveryDemands.DeliveryDemands[0]; //might be out of range

            contributions.Contributions.ForEach(contribution =>
            {
                if(contribution.Type == "Delivery")
                {
                    deliveryDemand.AddComponent(contribution);
                }
            });
            process.AddComponent(deliveryDemand);
            process.AddComponent(donorContribution);

            process.SetStore(_transactionStore);

            return process; 
        }

        private async Task<Campaign> BuildCampaign(string campaignId)
        {
            Campaign campaign = await _transactionStore.GetCampaign(campaignId);
            GetProcessesResult getProcessesResult = await _transactionStore.GetProcesses(campaignId);
            foreach(var process in getProcessesResult.Processes)
            {
                Process processTree = await BuildProcess(process.Id);
                campaign.AddComponent(processTree);
            }
            campaign.SetStore(_transactionStore);
            return campaign;
        }
    }
}
