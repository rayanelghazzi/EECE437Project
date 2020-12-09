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
        Task AddCampaign(Campaign campaign);
        Task EditCampaign(string campaignId, EditCampaignRequest request);
        Task DeleteCampaign(string campaignId);

        Task<DeliveryDemand> GetDeliveryDemand(string deliveryDemandId);
        Task<List<DeliveryDemand>> GetDeliveryDemands(GetDeliveryDemandsRequest request);
        Task AddDeliveryDemand(DeliveryDemand deliveryDemand);
        Task EditDeliveryDemand(string deliveryDemandId, EditDeliveryDemandRequest request);
        Task DeleteDeliveryDemand(string deliveryDemandId);

        Task<Contribution> GetContribution(string contributionId);
        Task<List<Contribution>> GetContributions(GetContributionsRequest request);
        Task EditContribution(string contributionId, EditContributionRequest request);
        Task DeleteContribution(string contributionId);

        Task<Process> GetProcess(string processId);
        Task<List<Campaign>> GetProcesses(string campaignId);
        Task EditProcess(string processId, Process process);
        Task DeleteProcess(string processId);

    }
}
