﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HumanityService.DataContracts
{
    public class EditContributionRequest
    {
        public long TimeWindowStart { get; set; }
        public long TimeWindowEnd { get; set; }
        public string OtherInfo { get; set; }
    }
}
