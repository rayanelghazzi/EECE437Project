﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HumanityService.Client.DataContracts.Requests
{
    public class AnswerCampaignRequest
    {
        public string Username { get; set; }
        public long TimeWindowStart { get; set; }
        public long TimeWindowEnd { get; set; }
        public string OtherInfo { get; set; }
    }
}
