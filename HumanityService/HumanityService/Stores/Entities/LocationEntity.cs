namespace HumanityService.Stores.Sql
{
    [System.ComponentModel.DataAnnotations.Schema.Table(TableName)]
    public class LocationEntity
    {
        public const string TableName = "locations";
        public string Description { get; set; }
        public string Coordinates { get; set; }
    }
}
