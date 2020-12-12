using System.Collections.Generic;

namespace HumanityService.Stores
{
    [System.ComponentModel.DataAnnotations.Schema.Table(TableName)]
    public class ContributionEntity
    {
        public const string TableName = "contributions";
        public int Id { get; set; }
        public int ProcessId { get; set; }
        public int DeliveryDemandId { get; set; }
        public string Type { get; set; }
        public string Username { get; set; }
        public string Status { get; set; }
        public long TimeWindowStart { get; set; }
        public long TimeWindowEnd { get; set; }
        public long TimeCreated { get; set; }
        public long TimeCompleted { get; set; }
    }
}
