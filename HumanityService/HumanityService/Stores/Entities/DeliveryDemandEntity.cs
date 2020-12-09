using System;
using System.Collections.Generic;
using System.Text;

namespace HumanityService.Stores
{
    [System.ComponentModel.DataAnnotations.Schema.Table(TableName)]
    public class DeliveryDemandEntity
    {
        public const string TableName = "DeliveryDemands";
        public string Name { get; set; }
        public string Id { get; set; }
        public string ProcessId { get; set; }
        public string Username { get; set; }
        public string TimeWindow { get; set; }
        public string Status { get; set; }
        public long TimeCreated { get; set; }
        public long TimeCompleted { get; set; }
        public string Description { get; set; }
    }
}
