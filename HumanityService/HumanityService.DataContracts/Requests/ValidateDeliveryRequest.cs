using System;
using System.Collections.Generic;
using System.Text;

namespace HumanityService.DataContracts.Requests
{
    public class ValidateDeliveryRequest
    {
        public string ContributionId { get; set; }
        public string DeliveryCode { get; set; }
    }
}
