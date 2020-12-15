using System.Collections.Generic;

namespace HumanityService.DataContracts
{
    /// <summary>
    /// DeliveryDemandId is only used if it is a delivery contribution
    /// DeliveryCode is only used if it is a delivery contribution
    /// </summary>

    public class Contribution
    {
        public string Id { get; set; }
        public string ProcessId { get; set; }
        public string DeliveryDemandId { get; set; }
        public string DeliveryCode { get; set; }
        public string Type { get; set; }
        public string Username { get; set; }
        public string Status { get; set; }
        public long TimeWindowStart { get; set; }
        public long TimeWindowEnd { get; set; }
        public string OtherInfo { get; set; }
        public long TimeCreated { get; set; }
        public long TimeCompleted { get; set; }
        public Location Location { get; set; }
    }

    /// <summary>
    /// Might use them later
    /// </summary>
    public enum ContributionStatus
    {
        Pending, 
        Accepted,
        PickedUp,
        Completed, 
        Canceled
    };
}
