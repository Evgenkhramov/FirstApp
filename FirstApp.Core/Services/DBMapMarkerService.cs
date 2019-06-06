using FirstApp.Core.Entities;
using FirstApp.Core.Interfaces;
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
            List<MapMarkerModel> list = _connect.Table<MapMarkerModel>().Where(x => x.TaskId == taskId).ToList();

            return list;
        }

        public void DeleteMarkers(int taskId)
        {
            _connect.Query<FileListModel>($"DELETE FROM MapMarker WHERE TaskId = {taskId}");
        }
    }
}
