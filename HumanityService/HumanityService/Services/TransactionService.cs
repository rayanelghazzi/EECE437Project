using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HumanityService.DataContracts;
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

        public TransactionService(ITransactionStore transactionStore, INotificationService notificationService, IMatchingService matchingService)
        {
            _transactionStore = transactionStore;
            _notificationService = notificationService;
            _matchingService = matchingService;
        }

        public Task<Campaign> FindMatch(string username, string type, string category)
        {
            throw new NotImplementedException();
            //call matching service with some parameters
            //what should we return (we have delivery demands and campaigns)
        }

        public async Task<string> AnswerCampaign(AnswerCampaignRequest request)
        {
            var campaign = await _transactionStore.GetCampaign(request.CampaignId);

            Process process = new Process
            {
                CampaignId = request.CampaignId,
                Status = "InProgress",
                TimeCreated = UnixTimeSeconds(),
                TimeCompleted = 0,
                DeliveryCode = Guid.NewGuid().ToString()
            };
            var processId = await _transactionStore.AddProcess(process);

            Contribution contribution = new Contribution
            {
                ProcessId = processId,
                Type = campaign.Type,
                Username = request.Username,
                Status = "Pending",
                TimeWindowStart = request.TimeWindowStart,
                TimeWindowEnd = request.TimeWindowEnd,
                OtherInfo = request.OtherInfo, 
                TimeCreated = UnixTimeSeconds(),
                TimeCompleted = 0
            };
            var contributionId = await _transactionStore.AddContribution(contribution);

            DeliveryDemand deliveryDemand = new DeliveryDemand
            {
                ProcessId = processId,
                CampaignName = campaign.Name,
                PickupUsername = request.Username,
                DestinationUsername = campaign.Username, 
                Status = "Pending",
                TimeWindowStart = request.TimeWindowStart,
                TimeWindowEnd = request.TimeWindowEnd,
                OtherInfo = request.OtherInfo,
                TimeCreated = UnixTimeSeconds(),
                TimeCompleted = 0
            };
            await _transactionStore.AddDeliveryDemand(deliveryDemand);

            return contributionId; 
        }

        public async Task<string> AnswerDeliveryDemand(AnswerDeliveryDemandRequest request)
        {
            //Update DeliveryDemand
            var deliveryDemand = await _transactionStore.GetDeliveryDemand(request.DeliveryDemandId);
            deliveryDemand.Status = "InProgress";
            await _transactionStore.UpdateDeliveryDemand(deliveryDemand);


            //add contribution to the delivery demand for the deliverer
            Contribution deliveryContribution = new Contribution
            {
                ProcessId = deliveryDemand.ProcessId,
                DeliveryDemandId = deliveryDemand.Id,
                Type = "Delivery",
                Username = request.Username,
                Status = "Accepted",
                TimeWindowStart = request.TimeWindowStart,
                TimeWindowEnd = request.TimeWindowEnd,
                OtherInfo = request.OtherInfo,
                TimeCreated = UnixTimeSeconds(),
                TimeCompleted = 0
            };

            var deliveryContributionId = await _transactionStore.AddContribution(deliveryContribution);

            //Update donor's contribution
            var donorContribution = await _transactionStore.GetContribution("TODO BY PROCESS ID");
            donorContribution.Status = "Accepted";

            return deliveryContributionId;
        }

        public Task ValidateDelivery(string processId, ValidateDeliveryRequest request)
        {
            throw new NotImplementedException();
            //tODOOOOOOOOOOf
        }

        public Task ApproveContribution(string contributionId)//
        {
            throw new NotImplementedException();
            //update campaign
            //
        }

        public async Task<string> CreateCampaign(CreateCampaignRequest request)
        {
            Campaign campaign = new Campaign
            {
                Name = request.Name,
                Username = request.NgoUsername,
                NgoName = request.NgoName,
                Type = request.Type,
                Category = request.Category,
                Target = request.Target,
                CurrentState = 0,
                TimeCreated = UnixTimeSeconds(),
                TimeCompleted = 0, 
                Description = request.Description
            };

            var campaignId = await _transactionStore.AddCampaign(campaign);
            return campaignId; 
        }

        public async Task DeleteCampaign(string campaignId)
        {
            await _transactionStore.DeleteCampaign(campaignId);
        }

        public async Task CancelContribution(string contributionId)
        {
            //If donation or volunteering delete the process (process, contributions, deliveryDemand)
            //
            //If delivery Demand update donation to pending and update delivery demand to
        }

        public Task DeleteDeliveryDemand(string deliveryDemandId)
        {
            throw new NotImplementedException();
            //Might need it if NGOs have the ability to change all these requirements
        }

        public Task EditCampaign(EditCampaignRequest request)
        {
            throw new NotImplementedException();
            // How do we only allow the right user to edit his camapaign
        }

        public Task EditContribution(EditContributionRequest request)
        {
            throw new NotImplementedException();
            //1st
            //edit the contribution
            //edit delivery demand if needed
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

        private long UnixTimeSeconds()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }
    }
}
