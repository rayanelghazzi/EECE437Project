using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumanityService.Services.Interfaces
{
    public interface IRoutingService
    {
        Task<long> GetETA(string delivererCoordinates, string donorCoordinates, string ngoCoordinates);
    }
}
