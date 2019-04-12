using FirstApp.Core.Interfaces;
using FirstApp.Core.Models;
using SQLite;
using System.Collections.Generic;

namespace FirstApp.Core.Services
{
    class DBMapMarkerService : IDBMapMarkerService
    {
        private SQLiteConnection _connect;
        public DBMapMarkerService(IDBConnectionService connecting)
        {
            _connect = connecting.GetDatebaseConnection();
            _connect.CreateTable<MapMarkerModel>();
        }

        public void AddMarkerToTable(MapMarkerModel marker)
        {
            _connect.Insert(marker);
        }

        public List<MapCoord> GetMapMarkerFromDB(int taskId)
        {
            List<MapCoord> coord = null;
            var list = _connect.Query<MapMarkerModel>($"SELECT * FROM MapMarker WHERE TaskId = {taskId}");
            for (int i = 0; i < list.Capacity; i++)
            {
                coord[i].CoordId = list[i].Id;
                coord[i].Lat = list[i].Lat;
                coord[i].Lng = list[i].Lng;
            }
            return coord;
        }
        public void DeleteMarker(int id)
        {
            _connect.Delete<MapMarkerModel>(id);
        }
    }
}
