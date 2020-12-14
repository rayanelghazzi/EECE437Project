using System;
using System.Collections.Generic;
using System.Text;

namespace HumanityService.DataContracts.Requests
{
    public class EditCampaignRequest
    {
        public string NgoUsername { get; set; }
        public string CampaignName { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public string Target { get; set; }
    }
}
