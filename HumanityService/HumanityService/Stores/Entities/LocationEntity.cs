namespace HumanityService.Stores
{
    [System.ComponentModel.DataAnnotations.Schema.Table(TableName)]
    public class LocationEntity
    {
        public const string TableName = "locations";
        public string Username { get; set; }
        public string Description { get; set; }
        public string Coordinates { get; set; }
    }
}
