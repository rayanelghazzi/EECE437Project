using System;
using System.Collections.Generic;

namespace HumanityService.DataContracts
{
    public class OpenrouteserviceMatrixResponse
    {
        public List<List<double>> durations { get; set; }
        public List<ORSLocation> destinations { get; set; }
        public List<ORSLocation> sources { get; set; }
    }

    public class ORSLocation
    {
        public List<double> location { get; set; }
        public double snapped_distance { get; set; }
    }
}
