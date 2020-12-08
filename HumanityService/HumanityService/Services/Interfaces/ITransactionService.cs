using HumanityService.DataContracts;
using HumanityService.DataContracts.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HumanityService.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<Campaign> GetCampaign(string campaignId);
        Task<List<Campaign>> GetCampaigns(GetCampaignsRequest request);
        Task CreateCampaign(CreateCampaignRequest request);
        Task EditCampaign(string campaignId, EditCampaignRequest request);
        Task DeleteCampaign(string campaignId);
        Task AnswerCampaign(string username, AnswerCampaignRequest request);

        Task<Campaign> GetDeliveryDemand(string deliveryDemandId);
        Task<List<Campaign>> GetDeliveryDemands(GetDeliveryDemandsRequest request);
        Task CreateDeliveryDemand(CreateDeliveryDemandRequest request);
        Task EditDeliveryDemand(string deliveryDemandId, EditDeliveryDemandRequest request);
        Task DeleteDeliveryDemand(string deliveryDemandId);
        Task AnswerDeliveryDemand(string username, AnswerDeliveryDemandRequest request);

        Task<Campaign> GetContribution(string contributionId);
        Task<List<Campaign>> GetContributions(GetContributionsRequest request);
        Task EditContribution(string contributionId, EditContributionRequest request);
        Task DeleteContribution(string contributionId);
        Task ApproveContribution(string contributionId);

        Task<Process> GetProcess(string processId);
        Task<List<Campaign>> GetProcesses(string campaignId);

        Task ValidateDelivery(string processId, ValidateDeliveryRequest request);
        Task AcceptDelivery(string contributionId);
    }
}
