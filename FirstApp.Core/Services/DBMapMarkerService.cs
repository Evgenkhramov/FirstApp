using FirstApp.Core.Interfaces;
using FirstApp.Core.Models;
using SQLite;

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

        public void AddFileNameToTable(MapMarkerModel marker)
        {
            _connect.Insert(marker);
        }

        public MapCoord[] GetMapMarkerFromDB(int taskId)
        {
            MapCoord[] coord = null;
            var list = _connect.Query<MapMarkerModel>($"SELECT * FROM MapMarker WHERE TaskId = {taskId}");
            for (int i = 0; i < list.Capacity; i++)
            {
                coord[i].Lat = list[i].Lat;
                coord[i].Lng = list[i].Lng;
            }
            return coord;
        }
        public void DeleteFileName(int id)
        {
            _connect.Delete<FileListModel>(id);
        }
    }
}
