using HumanityService.DataContracts;
using HumanityService.DataContracts.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumanityService.Stores.Interfaces
{
    public interface ITransactionStore
    {
        Task<Campaign> GetCampaign(string campaignId);
        Task<List<Campaign>> GetCampaigns(GetCampaignsRequest request);
        Task CreateCampaign(CreateCampaignRequest request);
        Task EditCampaign(string campaignId, EditCampaignRequest request);
        Task DeleteCampaign(string campaignId);

        Task<Campaign> GetDeliveryDemand(string deliveryDemandId);
        Task<List<Campaign>> GetDeliveryDemands(GetDeliveryDemandsRequest request);
        Task CreateDeliveryDemand(CreateDeliveryDemandRequest request);
        Task EditDeliveryDemand(string deliveryDemandId, EditDeliveryDemandRequest request);
        Task DeleteDeliveryDemand(string deliveryDemandId);

        Task<Campaign> GetContribution(string contributionId);
        Task<List<Campaign>> GetContributions(GetContributionsRequest request);
        Task EditContribution(string contributionId, EditContributionRequest request);
        Task DeleteContribution(string contributionId);

        Task<Process> GetProcess(string processId);
        Task<List<Campaign>> GetProcesses(string campaignId);
        Task EditProcess(string processId, Process process);
        Task DeleteProcess(string processId);

    }
}
