using HumanityService.DataContracts.CompositeDesignPattern;
using HumanityService.DataContracts.Requests;
using System.Threading.Tasks;

namespace HumanityService.Services.Interfaces
{
    public interface IMatchingService
    {
        Task<Campaign> MatchUserToCampaign(MatchCampaignRequest request);

        Task<DeliveryDemand> MatchUserToDeliveryDemand(MatchDeliveryDemandRequest request);
    }
}
