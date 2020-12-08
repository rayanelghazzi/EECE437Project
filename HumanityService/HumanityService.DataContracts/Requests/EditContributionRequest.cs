using System;
using System.Collections.Generic;
using System.Text;

namespace HumanityService.DataContracts.Requests
{
    public class EditContributionRequest
    {
        public List<long> TimeWindow { get; set; }
        public Location Location { get; set; }
    }
}
