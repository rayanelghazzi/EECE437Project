using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HumanityService.DataContracts;
using HumanityService.DataContracts.ContributionDataContracts;
using HumanityService.DataContracts.DemandDataContracts;
using HumanityService.DataContracts.ProcessDataContracts;
using HumanityService.Services;
using HumanityService.Services.Interfaces;

namespace HumanityService.Services
{
    public class TransactionService : ITransactionService
    {
        public Task AnswerDemand(string username, AnswerDemandRequest request)
        {
            throw new NotImplementedException();
        }

        public Task CancelContribution(string ContributionId)
        {
            throw new NotImplementedException();
        }

        public Task CancelDemand(string demandId)
        {
            throw new NotImplementedException();
        }

        public Task CreateDemand(string ngo, string demandName, string description, string type, string category, int target)
        {
            throw new NotImplementedException();
        }

        public Task EditContribution(string contributionId, List<long> timeWindow, List<Location> locations)
        {
            throw new NotImplementedException();
        }

        public Task EditDemand(string demandId, string demandName, string description, string type, string category, int target)
        {
            throw new NotImplementedException();
        }

        public Task<Contribution> GetContribution(string contributionId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Contribution>> GetContributions(GetContributionsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Demand> GetDemand(string demandId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Demand>> GetDemands(GetDemandsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Process> GetProcess(string processId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Contribution>> GetProcesses(string demandId)
        {
            throw new NotImplementedException();
        }

        public Task ValidateDelivery(string contributionId, string deliveryCode)
        {
            throw new NotImplementedException();
        }
    }
}
