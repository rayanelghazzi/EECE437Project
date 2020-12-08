using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HumanityService.DataContracts;
using HumanityService.DataContracts.Requests;
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

        public Task AcceptDelivery(string contributionId)
        {
            throw new NotImplementedException();
        }

        public Task AnswerCampaign(string username, AnswerCampaignRequest request)
        {
            throw new NotImplementedException();
        }

        public Task AnswerDeliveryDemand(string username, AnswerDeliveryDemandRequest request)
        {
            throw new NotImplementedException();
        }

        public Task ApproveContribution(string contributionId)
        {
            throw new NotImplementedException();
        }

        public Task CreateCampaign(CreateCampaignRequest request)
        {
            throw new NotImplementedException();
        }

        public Task CreateDeliveryDemand(CreateDeliveryDemandRequest request)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCampaign(string campaignId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteContribution(string contributionId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteDeliveryDemand(string deliveryDemandId)
        {
            throw new NotImplementedException();
        }

        public Task EditCampaign(string campaignId, EditCampaignRequest request)
        {
            throw new NotImplementedException();
        }

        public Task EditContribution(string contributionId, EditContributionRequest request)
        {
            throw new NotImplementedException();
        }

        public Task EditDeliveryDemand(string deliveryDemandId, EditDeliveryDemandRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Campaign> GetCampaign(string campaignId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Campaign>> GetCampaigns(GetCampaignsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Campaign> GetContribution(string contributionId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Campaign>> GetContributions(GetContributionsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Campaign> GetDeliveryDemand(string deliveryDemandId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Campaign>> GetDeliveryDemands(GetDeliveryDemandsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Process> GetProcess(string processId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Campaign>> GetProcesses(string campaignId)
        {
            throw new NotImplementedException();
        }

        public Task ValidateDelivery(string processId, ValidateDeliveryRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
