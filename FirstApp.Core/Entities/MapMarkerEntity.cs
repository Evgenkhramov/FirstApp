using SQLite;

namespace FirstApp.Core.Entities
{
    [Table("MapMarker")]
    public class MapMarkerEntity : BaseEntity
    {
        public int TaskId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
