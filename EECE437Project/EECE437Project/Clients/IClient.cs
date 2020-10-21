using EECE437Project.DataContract;
using System;
using System.Collections.Generic;
using System.Text;

namespace EECE437Project.Clients
{
    public interface IClient
    {
        List<Demand> GetDemands();
        Demand GetDemand(string demandId);
        void DeleteDemand(string demandId);
        List<Contribution> GetContribution();
        Contribution GetContribution(string demandId);
        void DeleteContributions(string demandId);
    }
}
