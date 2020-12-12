using System.Collections.Generic;

namespace HumanityService.DataContracts
{
    public class Contribution
    {
        public int Id { get; set; }
        public int ProcessId { get; set; }
        public int DeliveryDemandId { get; set; }
        public string Type { get; set; }
        public string Username { get; set; }
        public string Status { get; set; }
        public long TimeWindowStart { get; set; }
        public long TimeWindowEnd { get; set; }
        public long TimeCreated { get; set; }
        public long TimeCompleted { get; set; }
        public Location Location { get; set; }
    }
}
