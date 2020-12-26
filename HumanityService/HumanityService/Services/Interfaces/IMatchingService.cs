using HumanityService.DataContracts.CompositeDesignPattern;
using HumanityService.DataContracts.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HumanityService.Services.Interfaces
{
    public interface IMatchingService
    {
        Task<Campaign> MatchUserToCampaign(List<Campaign> campaigns, MatchCampaignRequest request);

        Task<DeliveryDemand> MatchUserToDeliveryDemand(List<DeliveryDemand> deliveryDemands, MatchDeliveryDemandRequest request);
    }
}
