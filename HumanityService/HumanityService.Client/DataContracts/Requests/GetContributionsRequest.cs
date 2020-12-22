using System;
using System.Collections.Generic;
using System.Text;

namespace HumanityService.DataContracts
{
    public class GetContributionsRequest
    {
        public string Username { get; set; }
        public string ProcessId { get; set; }
        public string DeliveryDemandId { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
    }
}
