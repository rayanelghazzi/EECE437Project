﻿using HumanityService.DataContracts.CompositeDesignPattern;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanityService.Client.DataContracts.Results
{
    public class GetContributionsResult
    {
        public List<Contribution> Contributions { get; set; }
    }
}
