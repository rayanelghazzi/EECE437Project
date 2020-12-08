using System;
using System.Collections.Generic;
using System.Text;

namespace HumanityService.DataContracts.Requests
{
    public class AnswerDeliveryDemandRequest
    {
        public string DeliveryDemandId { get; set; }
        public List<long> TimeWindow { get; set; }
        public Location Location { get; set; }
        public string OtherInfo { get; set; }
    }
}
