using FirstApp.Core.Entities;
using System.Collections.Generic;

namespace FirstApp.Core.Interfaces
{
    public interface IMarkersRepository
    {
        void InsertMarkers(List<MapMarkerEntity> list);
        void Insert(MapMarkerEntity marker);
        List<MapMarkerEntity> GetMarkersList(int taskId);
        void DeleteMarkers(int taskId);
    }
}
