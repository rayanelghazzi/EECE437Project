﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EECE437Project.DataContract
{
    public class Process
    {
        public string Id { get; set; }
        public string DemandId { get; set; }
        public string Status { get; set; }
        public long TimeCreated { get; set; }
        public long TimeCompleted { get; set; }
        public string DeliveryCode { get; set; }
    }
}