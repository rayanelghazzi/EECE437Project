using HumanityService.DataContracts.CompositeDesignPattern;
using HumanityService.DataContracts.Requests;
using System.Threading.Tasks;

namespace HumanityService.Services.Interfaces
{
    public interface IMatchingService
    {
        Task<Campaign> MatchUserToCampaign(GetCampaignsRequest request);

        Task<DeliveryDemand> MatchUserToDeliveryDemand(GetDeliveryDemandsRequest request);
    }
}
