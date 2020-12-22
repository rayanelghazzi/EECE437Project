using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumanityService.DataContracts
{
    public class OpenrouteserviceMatrixRequest
    {
        public List<List<double>> locations { get; set; }
    }
}
