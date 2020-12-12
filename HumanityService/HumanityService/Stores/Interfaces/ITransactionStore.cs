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
        Task<Campaign> GetCampaign(int campaignId);
        Task<List<Campaign>> GetCampaigns(GetCampaignsRequest request);
        Task AddCampaign(Campaign campaign);
        Task UpdateCampaign(Campaign campaign);
        Task DeleteCampaign(int campaignId);

        Task<DeliveryDemand> GetDeliveryDemand(int deliveryDemandId);
        Task<List<DeliveryDemand>> GetDeliveryDemands(GetDeliveryDemandsRequest request);
        Task AddDeliveryDemand(DeliveryDemand deliveryDemand);
        Task UpdateDeliveryDemand(DeliveryDemand deliveryDemand);
        Task DeleteDeliveryDemand(int deliveryDemandId);

        Task<Contribution> GetContribution(int contributionId);
        Task<List<Contribution>> GetContributions(GetContributionsRequest request);
        Task AddContribution(Contribution contribution);
        Task UpdateContribution(Contribution request);
        Task DeleteContribution(int contributionId);

        Task<Process> GetProcess(int processId);
        Task<List<Campaign>> GetProcesses(int campaignId);
        Task AddProcess(Process process);
        Task UpdateProcess(Process process);
        Task DeleteProcess(int processId);
    }
}
