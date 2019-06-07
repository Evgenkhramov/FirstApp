using FirstApp.Core.Entities;
using System.Collections.Generic;

namespace FirstApp.Core.Interfaces
{
    public interface IMapMarkerService
    {
        void AddMarkerToTable(MapMarkerEntity marker);
        List<MapMarkerEntity> GetMapMarkerListFromDB(int taskId);
        void DeleteMarkers(int taskId);
    }
}
