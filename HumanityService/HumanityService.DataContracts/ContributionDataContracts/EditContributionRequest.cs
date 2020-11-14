using System;
using System.Collections.Generic;
using System.Text;

namespace HumanityService.DataContracts.ContributionDataContracts
{
    public class EditContributionRequest
    {
        public List<long> TimeWidown { get; set; }
        public List<Location> Locations { get; set; }
    }
}
