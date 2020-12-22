using HumanityService.DataContracts.CompositeDesignPattern;
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
        private readonly ITransactionService _transactionService;
        
        public MatchingService(IRoutingService routingService, ITransactionService transactionService)
        {
            _routingService = routingService;
            _transactionService = transactionService;
        }

        public async Task<Campaign> MatchUserToCampaign(MatchCampaignRequest request)
        {
            var getCampaignsRequest = new GetCampaignsRequest
            {
                Status = "Active",
                Type = request.Type,
                Category = request.Category
            };
            var getCampaignsResult = await _transactionService.GetCampaigns(getCampaignsRequest);
            var campaigns = getCampaignsResult.Campaigns;
            if (campaigns.Count == 0) return null;

            if(request.Type == "Donation")
            {
                List<Campaign> sortedCampaigns = campaigns.OrderBy(x => x.CompletedCount).ToList();
                return sortedCampaigns[0];
            }
            else
            {
                var campaignsETA = new List<(double, Campaign)>();
                foreach (var campaign in campaigns)
                {
                    var eta = await _routingService.GetETA(request.Location, campaign.Location, request.TransportationType);
                    campaignsETA.Add((eta, campaign));
                }
                if (campaignsETA.Count == 0) return null;

                //Sort the campaigns from closest to farthest and choose the closest
                campaignsETA.Sort();
                return campaignsETA[0].Item2;
            }
        }

        //change params: we need Deliverer's location, donor's location, and destination location + his time range + deliverer transportation (e.g car, pedestrian, )
        public async Task<DeliveryDemand> MatchUserToDeliveryDemand(MatchDeliveryDemandRequest request) 
        {
            //Get all pending delivery demands
            var getDeliveryDemandsRequest = new GetDeliveryDemandsRequest
            {
                Status = "Pending"
            };
            var getDeliveryDemandsResult = await _transactionService.GetDeliveryDemands(getDeliveryDemandsRequest);


            // We filter out all active donation contributions that are not included in this time range
            var deliveryDemands = new List<DeliveryDemand>();

            foreach(var deliveryDemand in getDeliveryDemandsResult.DeliveryDemands)
            {
                if(deliveryDemand.TimeWindowEnd > request.TimeWindowStart)
                {
                    deliveryDemands.Add(deliveryDemand);
                }
            }

            //we get the estimated duration time that it would take for the deliverer to complete each delivery demand
            var deliveryDemandsETA = new List<(double, DeliveryDemand)>();
            foreach(var deliveryDemand in deliveryDemands)
            {
                var eta = await _routingService.GetETA(request.DelivererLocation, deliveryDemand.PickupLocation, deliveryDemand.DestinationLocation, request.TransportationType);
                if(request.TimeWindowStart + eta < request.TimeWindowEnd && request.TimeWindowStart + eta < deliveryDemand.TimeWindowEnd) //compatible with deliverer's and donor's time range
                {
                    deliveryDemandsETA.Add((eta, deliveryDemand));
                }
            }
            if (deliveryDemands.Count == 0) return null;


            //Sort the delivery demands from least to most time consuming to deliverer
            deliveryDemandsETA.Sort();
            
            return deliveryDemandsETA[0].Item2;
        }
    }
}
