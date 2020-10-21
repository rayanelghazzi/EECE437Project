using EECE437Project.DataContract;
using System;
using System.Collections.Generic;
using System.Text;

namespace EECE437Project.Clients
{
    public interface IUserClient
    {
        List<Demand> GetDemands();
        Demand GetDemand(string demandId);
        void DeleteDemand(string demandId);
        List<Task> GetTasks();
        Task GetTask(string demandId);
        void DeleteTask(string demandId);
    }
}
