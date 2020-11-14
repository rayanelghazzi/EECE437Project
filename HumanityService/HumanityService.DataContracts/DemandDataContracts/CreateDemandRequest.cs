using System;
using System.Collections.Generic;
using System.Text;

namespace HumanityService.DataContracts.DemandDataContracts
{
    public class CreateDemandRequest
    {
        public string Ngo { get; set; }
        public string DemandName { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public string Target { get; set; }
    }
}
