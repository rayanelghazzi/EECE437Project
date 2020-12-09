using System;
using System.Collections.Generic;
using System.Text;

namespace HumanityService.DataContracts
{
    public class DeliveryDemand
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string ProcessId { get; set; }
        public string Username { get; set; }
        public Location Location { get; set; }
        public List<long> TimeWindow { get; set; }
        public string Status { get; set; }
        public long TimeCreated { get; set; }
        public long TimeCompleted { get; set; }
        public string Description { get; set; }
    }
}
