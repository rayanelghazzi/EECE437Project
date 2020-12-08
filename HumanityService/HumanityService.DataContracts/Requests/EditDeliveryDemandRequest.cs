using System;
using System.Collections.Generic;
using System.Text;

namespace HumanityService.DataContracts.Requests
{
    public class EditDeliveryDemandRequest
    {
        public List<long> TimeWindow { get; set; }
        public Location donorLocation { get; set; }
        public Location ngoLocation { get; set; }
        public string Status { get; set; }
    }
}
