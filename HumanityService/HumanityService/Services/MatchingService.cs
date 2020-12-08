using HumanityService.DataContracts;
using HumanityService.DataContracts.Requests;
using HumanityService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumanityService.Services
{
    public class MatchingService : IMatchingService
    {
        private readonly IRoutingService _routingService;
        
        public MatchingService(IRoutingService routingService)
        {
            _routingService = routingService;
        }

        public Task<Campaign> MatchUserToCampaign(GetCampaignsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<DeliveryDemand> MatchUserToDeliveryDemand(GetDeliveryDemandsRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
