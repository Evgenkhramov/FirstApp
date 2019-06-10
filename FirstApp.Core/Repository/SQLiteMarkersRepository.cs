using FirstApp.Core.Entities;
using FirstApp.Core.Interfaces;
using SQLite;
using System.Collections.Generic;

namespace FirstApp.Core.Repository
{
    public class SQLiteMapMarkersRepository : BaseRepository<MapMarkerEntity>, IMarkersRepository
    {
        private readonly SQLiteConnection _connect;

        public SQLiteMapMarkersRepository(IConnectionService connecting) : base(connecting)
        {
            _connect = connecting.GetDatebaseConnection();
            _connect.CreateTable<MapMarkerEntity>();
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
            _connect.Table<MapMarkerEntity>().Where(x => x.TaskId == taskId);
        }
    }
}
