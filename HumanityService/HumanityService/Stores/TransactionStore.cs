using HumanityService.DataContracts;
using HumanityService.DataContracts.Requests;
using HumanityService.Stores.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumanityService.Stores
{
    public class TransactionStore : ITransactionStore
    {
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

        public Task DeleteProcess(string processId)
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

        public Task EditProcess(string processId, Process process)
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
    }
}
