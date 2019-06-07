using FirstApp.Core.Entities;
using FirstApp.Core.Interfaces;
using SQLite;
using System.Collections.Generic;

namespace FirstApp.Core.Services
{
    class MapMarkerService : IMapMarkerService
    {
        private readonly SQLiteConnection _connect;

        public MapMarkerService(IConnectionService connecting)
        {
            _connect = connecting.GetDatebaseConnection();
            _connect.CreateTable<MapMarkerEntity>();
        }

        public void AddMarkerToTable(MapMarkerEntity marker)
        {
            _connect.Insert(marker);
        }

        public List<MapMarkerEntity> GetMapMarkerListFromDB(int taskId)
        {
            List<MapMarkerEntity> list = _connect.Table<MapMarkerEntity>().Where(x => x.TaskId == taskId).ToList();

            return list;
        }

        public void DeleteMarkers(int taskId)
        {
            _connect.Query<FileListEntity>($"DELETE FROM MapMarker WHERE TaskId = {taskId}");
        }
    }
}
