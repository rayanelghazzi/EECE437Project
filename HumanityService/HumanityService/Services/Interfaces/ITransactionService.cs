using HumanityService.DataContracts;
using HumanityService.DataContracts.ContributionDataContracts;
using HumanityService.DataContracts.DemandDataContracts;
using HumanityService.DataContracts.ProcessDataContracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HumanityService.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<Demand> GetDemand(string demandId);
        Task<List<Demand>> GetDemands(GetDemandsRequest request);
        Task CreateDemand(CreateDemandRequest request);
        Task EditDemand(string demandId, EditDemandRequest request);
        Task CancelDemand(string demandId);
        Task AnswerDemand(string username, AnswerDemandRequest request);
        Task<Contribution> GetContribution(string contributionId);
        Task<List<Contribution>> GetContributions(GetContributionsRequest request);
        Task EditContribution(string contributionId, EditContributionRequest request);
        Task CancelContribution(string contributionId);
        Task<Process> GetProcess(string processId);
        Task<List<Contribution>> GetProcesses(string demandId);
        Task ValidateDelivery(ValidateDeliveryRequest request); //what abt ngo's side?
        Task AcceptDelivery(string contributionId);
        Task ApproveContribution(string contributionId);
    }
}
