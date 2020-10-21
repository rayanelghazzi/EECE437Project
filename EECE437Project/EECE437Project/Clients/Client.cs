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

        public void AnswerDemand(string demandId)
        {
            throw new NotImplementedException();
        }

        public void CancelContribution(string demandId)
        {
            throw new NotImplementedException();
        }

        public void CancelDemand(string demandId)
        {
            throw new NotImplementedException();
        }

        public void CreateDemand()
        {
            throw new NotImplementedException();
        }

        public void EditContribution()
        {
            throw new NotImplementedException();
        }

        public void EditDemand()
        {
            throw new NotImplementedException();
        }

        public Contribution GetContribution(string demandId)
        {
            throw new NotImplementedException();
        }

        public List<Contribution> GetContributions()
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

        public Process GetProcess()
        {
            throw new NotImplementedException();
        }

        public List<Process> GetProcesses()
        {
            throw new NotImplementedException();
        }

        public bool ValidateDelivery(string deliveryCode)
        {
            throw new NotImplementedException();
        }
    }
}
