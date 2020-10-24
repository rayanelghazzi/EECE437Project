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
        Task CreateDemand(string ngo, string demandName, string description, string type, string category, int target);
        Task EditDemand(string demandId, string demandName, string description, string type, string category, int target);
        Task CancelDemand(string demandId);
        Task AnswerDemand(string username, AnswerDemandRequest request);
        Task<Contribution> GetContribution(string contributionId);
        Task<List<Contribution>> GetContributions(GetContributionsRequest request);
        Task EditContribution(string contributionId, List<long> timeWindow, List<Location> locations);
        Task CancelContribution(string ContributionId); 
        Task<Process> GetProcess(string processId);
        Task<List<Contribution>> GetProcesses(string demandId);
        Task ValidateDelivery(string contributionId, string deliveryCode); //what abt ngo's side?
    }
}
