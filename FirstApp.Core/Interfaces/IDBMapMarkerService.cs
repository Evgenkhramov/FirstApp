using FirstApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstApp.Core.Interfaces
{
    public interface IDBMapMarkerService
    {
        void AddMarkerToTable(MapMarkerModel marker);
        List<MapCoord> GetMapMarkerFromDB(int taskId);
        void DeleteMarker(int id);
    }
}
