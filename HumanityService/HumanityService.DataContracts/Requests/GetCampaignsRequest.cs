using System;
using System.Collections.Generic;
using System.Text;

namespace HumanityService.DataContracts.Requests
{
    public class GetCampaignsRequest
    {
        public string Ngo { get; set; }
        public string ProcessId { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
    }
}
