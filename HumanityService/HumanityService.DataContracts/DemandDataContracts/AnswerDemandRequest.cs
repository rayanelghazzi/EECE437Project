using System;
using System.Collections.Generic;
using System.Text;

namespace HumanityService.DataContracts.DemandDataContracts
{
    public class AnswerDemandRequest
    {
        public string DemandId { get; set; }
        public List<long> TimeWindow { get; set; }
        public Location Location { get; set; }
        public string OtherInfo { get; set; }
    }
}
