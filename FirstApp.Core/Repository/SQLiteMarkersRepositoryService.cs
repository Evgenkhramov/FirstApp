using FirstApp.Core.Entities;
using FirstApp.Core.Interfaces;
using SQLite;
using System.Collections.Generic;

namespace FirstApp.Core.Repository
{
    public class SQLiteMapMarkersRepositoryService : IMarkersRepositoryService
    {
        private readonly SQLiteConnection _connect;

        public SQLiteMapMarkersRepositoryService(IConnectionService connecting)
        {
            _connect = connecting.GetDatebaseConnection();
            _connect.CreateTable<MapMarkerEntity>();
        }

        public void InsertMarker(MapMarkerEntity marker)
        {
            _connect.Insert(marker);
        }

        public void InsertMarkers(List<MapMarkerEntity> list)
        {
            _connect.InsertAll(list);
        }

        public List<MapMarkerEntity> GetMarkersList(int taskId)
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
