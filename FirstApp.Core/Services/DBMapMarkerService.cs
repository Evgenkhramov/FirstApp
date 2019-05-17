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

        public List<MapMarkerModel> GetMapMarkerListFromDB(int taskId)
        {
            List<MapMarkerModel> coord;
            coord = new List<MapMarkerModel>();
            var list = _connect.Query<MapMarkerModel>($"SELECT ALL * FROM MapMarker WHERE TaskId = {taskId}");
            if (list.Count > 0)
            {
                foreach (MapMarkerModel item in list)
                {
                    MapMarkerModel row = new MapMarkerModel();
                    row.Id = item.Id;
                    row.TaskId = item.TaskId;
                    row.Lat = item.Lat;
                    row.Lng = item.Lng;
                    coord.Add(row);
                }
            }

            return coord;
        }

        public void DeleteMarker(int id)
        {
            _connect.Delete<MapMarkerModel>(id);
        }
    }
}
