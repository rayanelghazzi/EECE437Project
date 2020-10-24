using System.Collections.Generic;

namespace HumanityService.DataContracts.ContributionDataContracts
{
    public class Contribution
    {
        public string Id { get; set; }
        public string ProcessId { get; set; }
        public string Type { get; set; }
        public string User { get; set; }
        public string Status { get; set; }
        public List<long> TimeWindow { get; set; }
        public long TimeCreated { get; set; }
        public long TimeCompleted { get; set; }
        public List<Location> Locations { get; set; }
    }
}
