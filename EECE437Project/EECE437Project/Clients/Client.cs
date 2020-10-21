using System;
using System.Collections.Generic;
using EECE437Project.Clients;
using EECE437Project.DataContract;

namespace EECE437Project.Services
{
    public class Client : IClient
    {
        public Client()
        {
        }

        public void DeleteContributions(string demandId)
        {
            throw new NotImplementedException();
        }

        public void DeleteDemand(string demandId)
        {
            throw new NotImplementedException();
        }

        public List<Contribution> GetContribution()
        {
            throw new NotImplementedException();
        }

        public Contribution GetContribution(string demandId)
        {
            throw new NotImplementedException();
        }

        public Demand GetDemand(string demandId)
        {
            throw new NotImplementedException();
        }

        public List<Demand> GetDemands()
        {
            throw new NotImplementedException();
        }
    }
}
