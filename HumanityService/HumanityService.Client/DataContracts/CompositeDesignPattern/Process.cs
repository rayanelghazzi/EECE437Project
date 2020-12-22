using HumanityService.DataContracts.Requests;
using HumanityService.Stores.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HumanityService.Client.DataContracts
{
    public class Process
    {
        public string Id { get; set; }
        public string CampaignId { get; set; }
        public string Status { get; set; }
        public long TimeCreated { get; set; }
        public long TimePickedUp { get; set; }
        public long TimeCompleted { get; set; }
        public string DeliveryCode { get; set; }
    }
}
