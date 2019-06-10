using FirstApp.Core.Entities;
using System.Collections.Generic;

namespace FirstApp.Core.Interfaces
{
    public interface IMarkersRepository : IBaseRepository<MapMarkerEntity>
    {
        void InsertMarkers(List<MapMarkerEntity> list);
        List<MapMarkerEntity> GetMarkersList(int taskId);
        void DeleteMarkers(int taskId);
    }
}
