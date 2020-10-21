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

        public Demand GetDemand(string demandId)
        {
            throw new NotImplementedException();
        }

        public List<Demand> GetDemands()
        {
            throw new NotImplementedException();
        }

        public void DeleteDemand(string demandId)
        {
            throw new NotImplementedException();
        }

        public void DeleteTask(string demandId)
        {
            throw new NotImplementedException();
        }

        public Task GetTask(string demandId)
        {
            throw new NotImplementedException();
        }

        public List<Task> GetTasks()
        {
            throw new NotImplementedException();
        }
    }
}
