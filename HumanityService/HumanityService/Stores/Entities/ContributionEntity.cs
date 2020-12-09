using System.Collections.Generic;

namespace HumanityService.Store
{
    [System.ComponentModel.DataAnnotations.Schema.Table(TableName)]
    public class ContributionEntity
    {
        public const string TableName = "contributions";
        public string Id { get; set; }
        public string ProcessId { get; set; }
        public string Type { get; set; }
        public string Username { get; set; }
        public string Status { get; set; }
        public string TimeWindow { get; set; }
        public long TimeCreated { get; set; }
        public long TimeCompleted { get; set; }
    }
}
