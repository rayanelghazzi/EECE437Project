using System;
namespace EECE437Project.Services
{
    public interface ITransactionService
    {
        void GetDemand();
        void GetDemands();
        void CreateDemand();
        void EditDemand();
        void CancelDemand();
        void GetContribution();
        void GetContributions();
        void EditContribution();
        void CancelContribution();
        void AnswerDemand();
        void GetProcess();
        void GetProcesses();
        void ValidateDelivery(); //Validate the delivery code + Complete Contribution
    }
}
