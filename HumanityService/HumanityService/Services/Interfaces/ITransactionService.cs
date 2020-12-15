using HumanityService.DataContracts;
using HumanityService.DataContracts.Requests;
using HumanityService.DataContracts.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HumanityService.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<Campaign> FindMatch(string username, string type, string category);

        Task<Campaign> GetCampaign(string campaignId);
        Task<GetCampaignsResult> GetCampaigns(GetCampaignsRequest request);
        Task<string> CreateCampaign(CreateCampaignRequest request);
        Task EditCampaign(EditCampaignRequest request);
        Task DeleteCampaign(string campaignId);
        Task<string> AnswerCampaign(AnswerCampaignRequest request);

        Task<DeliveryDemand> GetDeliveryDemand(string deliveryDemandId);
        Task<GetDeliveryDemandsResult> GetDeliveryDemands(GetDeliveryDemandsRequest request);
        Task DeleteDeliveryDemand(string deliveryDemandId);
        Task<string> AnswerDeliveryDemand(AnswerDeliveryDemandRequest request);

        Task<Contribution> GetContribution(string contributionId);
        Task<GetContributionsResult> GetContributions(GetContributionsRequest request);
        Task EditContribution(EditContributionRequest request);
        Task DeleteContribution(string contributionId);
        Task ApproveContribution(string contributionId);

        Task<Process> GetProcess(string processId);
        Task<GetProcessesResult> GetProcesses(string campaignId);

        Task<bool> ValidateDelivery(ValidateDeliveryRequest request);
    }
}
