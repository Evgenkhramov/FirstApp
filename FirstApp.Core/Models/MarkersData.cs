using System.Collections.Generic;

namespace FirstApp.Core.Entities
{
    public class MarkersData
    {
        public int TaskId { get; set; }
        public List<MapMarkerEntity> Markers { get; set; }

        public MarkersData()
        {
            Markers = new List<MapMarkerEntity>();
        }
    }
}
