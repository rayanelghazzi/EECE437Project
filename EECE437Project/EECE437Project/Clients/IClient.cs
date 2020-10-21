using EECE437Project.Components;
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
        void CancelDemand(string demandId);
        void CreateDemand();
        void EditDemand();
        List<Contribution> GetContributions();
        Contribution GetContribution(string demandId);
        void CancelContribution(string demandId);
        void EditContribution();
        void AnswerDemand(string demandId);
        Process GetProcess();
        List<Process> GetProcesses();
        bool ValidateDelivery(string deliveryCode);


    }
}
