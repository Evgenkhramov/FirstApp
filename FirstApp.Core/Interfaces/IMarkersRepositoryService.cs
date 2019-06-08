using FirstApp.Core.Entities;
using System.Collections.Generic;

namespace FirstApp.Core.Interfaces
{
    public interface IMarkersRepositoryService
    {
        void InsertMarkers(List<MapMarkerEntity> list);
        void InsertMarker(MapMarkerEntity marker);
        List<MapMarkerEntity> GetMarkersList(int taskId);
        void DeleteMarkers(int taskId);
    }
}
