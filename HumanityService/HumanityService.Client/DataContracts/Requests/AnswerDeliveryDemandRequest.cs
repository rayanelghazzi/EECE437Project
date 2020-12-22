using HumanityService.DataContracts.CompositeDesignPattern;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanityService.Client.DataContracts
{
    public class AnswerDeliveryDemandRequest
    {
        public string Username { get; set; }
        public long TimeWindowStart { get; set; }
        public long TimeWindowEnd { get; set; }
        public Location Location { get; set; }
        public string OtherInfo { get; set; }
    }
}
