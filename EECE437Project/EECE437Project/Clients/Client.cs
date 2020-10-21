using System;
using System.Collections.Generic;
using EECE437Project.Components;

namespace EECE437Project.Services
{
    public class UserClient
    {
        public UserClient()
        {
            List<Demand> GetDemands()
            {
                return new List<Demand>();
            }
            Demand GetDemand(string demandId)
            {
                return new Demand();
            }
            void DeleteDemand(string demandId)
            {

            }

            List<Task> GetTasks()
            {
                return new List<Task>();
            }
            Task GetTask(string demandId)
            {
                return new Task();
            }
            void DeleteTask(string demandId)
            {

            }
        }
    }
}
