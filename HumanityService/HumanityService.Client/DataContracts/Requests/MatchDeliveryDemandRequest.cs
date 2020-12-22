﻿using HumanityService.DataContracts.CompositeDesignPattern;


namespace HumanityService.Client.DataContracts.Requests
{
    public class MatchDeliveryDemandRequest
    {
        public Location DelivererLocation { get; set; } 
        public string TransportationType { get; set; }
        public long TimeWindowStart { get; set; }
        public long TimeWindowEnd { get; set; }
    }
}
