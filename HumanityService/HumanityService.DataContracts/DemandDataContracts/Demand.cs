using System.Collections.Generic;

namespace HumanityService.DataContracts.DemandDataContracts
{
    public class Demand
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string User { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public int Target { get; set; }
        public int CurrentState { get; set; }
        public long TimeCreated { get; set; }
        public long TimeCompleted { get; set; }
        public string Description { get; set; }
        public List<Location> Locations {get; set;}

    }
}
