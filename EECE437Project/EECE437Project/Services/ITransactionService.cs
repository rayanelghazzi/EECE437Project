using System;
namespace EECE437Project.Services
{
    public interface ITransactionService
    {
        void CreateDemand();
        void EditDemand();
        void DeleteDemand();
        void CreateTask();
        void EditTask();
        void DeleteTask();
    }
}
