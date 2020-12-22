﻿using HumanityService.DataContracts.Requests;
using HumanityService.Stores.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HumanityService.Client.DataContracts
{
    public class Contribution
    {
        public string Id { get; set; }
        public string ProcessId { get; set; }
        public string DeliveryDemandId { get; set; }
        public string DeliveryCode { get; set; }
        public string Type { get; set; }
        public string Username { get; set; }
        public string Status { get; set; }
        public long TimeWindowStart { get; set; }
        public long TimeWindowEnd { get; set; }
        public string OtherInfo { get; set; }
        public long TimeCreated { get; set; }
        public long TimeCompleted { get; set; }
        public Location Location { get; set; }
    }
}
