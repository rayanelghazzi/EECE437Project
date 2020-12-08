using HumanityService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumanityService.Services
{
    public class RoutingService : IRoutingService
    {
        public Task<long> GetETA(string delivererCoordinates, string donorCoordinates, string ngoCoordinates)
        {
            throw new NotImplementedException();
        }
    }
}
