
using System.Collections.Generic;
using System.Text;

namespace HumanityService.DataContracts { 
    public class CreateDeliveryDemandRequest
    {
        public string Id { get; set; }
        public string ProcessId { get; set; }
        public List<long> TimeWindow { get; set; }
        public Location donorLocation { get; set; }
        public Location ngoLocation { get; set; }
        public string Status { get; set; }
    }
}
