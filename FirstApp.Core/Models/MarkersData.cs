using System.Collections.Generic;

namespace FirstApp.Core.Models
{
    public class MarkersData
    {
        public int TaskId { get; set; }
        public List<MapMarkerModel> Markers;

        public MarkersData()
        {
            Markers = new List<MapMarkerModel>();
        }
    }
}
