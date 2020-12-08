using HumanityService.DataContracts;
using HumanityService.DataContracts.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumanityService.Services.Interfaces
{
    public interface IMatchingService
    {
        Task<Campaign> MatchUserToCampaign(GetCampaignsRequest request);

        Task<DeliveryDemand> MatchUserToDeliveryDemand(GetDeliveryDemandsRequest request);
    }
}
