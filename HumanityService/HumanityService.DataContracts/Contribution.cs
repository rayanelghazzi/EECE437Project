using System.Collections.Generic;

namespace HumanityService.DataContracts
{
    public class Contribution
    {
        public string Id { get; set; }
        public string ProcessId { get; set; }
        public string Type { get; set; }
        public string Username { get; set; }
        public string Status { get; set; }
        public List<long> TimeWindow { get; set; }
        public long TimeCreated { get; set; }
        public long TimeCompleted { get; set; }
        public Location Location { get; set; }
    }
}
