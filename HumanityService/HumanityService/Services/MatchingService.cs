using HumanityService.DataContracts.CompositeDesignPattern;
using HumanityService.DataContracts.Requests;
using HumanityService.Services.Interfaces;
using System;
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
            ////Volunteering
            //Get volunteering campaigns with similar type and category
            //Get the time required to get to each one
            //Idk what else

            //Donations 
            //Get volunteering campaigns with similar type and category
            //Return campaign with smallest reach/target ratio
        }

        public Task<DeliveryDemand> MatchUserToDeliveryDemand(GetDeliveryDemandsRequest request)
        {
            throw new NotImplementedException();
            // Volunteer uploads his location + time range
            // We filter out all active donation contributions that are not included in this time range
            // get the time required for each contribution and return only those who remain in his time range
            // Pick the oldest one
        }
    }
}
