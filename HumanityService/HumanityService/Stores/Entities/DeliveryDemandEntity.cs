using System;
using System.Collections.Generic;
using System.Text;

namespace HumanityService.Stores
{
    [System.ComponentModel.DataAnnotations.Schema.Table(TableName)]
    public class DeliveryDemandEntity
    {
        public const string TableName = "DeliveryDemands";

        public int Id { get; set; }
        public string Name { get; set; }
        public int ProcessId { get; set; }
        public string PickupUsername { get; set; }
        public string DestinationUsername { get; set; }
        public string Status { get; set; }
        public long TimeWindowStart { get; set; }
        public long TimeWindowEnd { get; set; }
        public long TimeCreated { get; set; }
        public long TimeCompleted { get; set; }
        public string Description { get; set; }
    }
}
