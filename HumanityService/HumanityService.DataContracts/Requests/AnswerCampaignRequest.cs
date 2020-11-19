using System;
using System.Collections.Generic;
using System.Text;

namespace HumanityService.DataContracts.Requests
{
    public class AnswerCampaignRequest
    {
        public string CampaignId { get; set; }
        public List<long> TimeWindow { get; set; }
        public Location Location { get; set; }
        public string OtherInfo { get; set; }
    }
}
