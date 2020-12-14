
namespace HumanityService.DataContracts
{
    public class Process
    {
        public string Id { get; set; }
        public string CampaignId { get; set; }
        public string Status { get; set; }
        public long TimeCreated { get; set; }
        public long TimeCompleted { get; set; }
        public string DeliveryCode { get; set; }
    }

    /// <summary>
    /// Might use them later
    /// </summary>
    public enum ProcessStatus
    {
        InProgress,
        PickepUp,
        Completed,
        Canceled
    };

}
