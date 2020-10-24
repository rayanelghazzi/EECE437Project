using System;
using System.Collections.Generic;
using System.Text;

namespace HumanityService.DataContracts.ContributionDataContracts
{
    public class GetContributionsRequest
    {
        public string Username { get; set; }
        public string ProcessId { get; set; }
    }
}
