using SQLite;

namespace FirstApp.Core.Models
{
    [Table("MapMarker")]
    public class MapMarkerModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int TaskId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
