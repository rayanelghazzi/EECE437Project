using HumanityService.DataContracts.CompositeDesignPattern;
using System;
using System.Collections.Generic;

namespace HumanityService.Client.DataContracts.Results
{
    public class GetCampaignsResult
    {
        public List<Campaign> Campaigns { get; set; }
    }
}
