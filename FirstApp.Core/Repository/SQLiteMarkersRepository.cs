using FirstApp.Core.Entities;
using FirstApp.Core.Interfaces;
using SQLite;
using System.Collections.Generic;

namespace FirstApp.Core.Repository
{
    public class SQLiteMapMarkersRepository : IMarkersRepository, IBaseRepository<MapMarkerEntity>
    {
        private readonly SQLiteConnection _connect;

        public SQLiteMapMarkersRepository(IConnectionService connecting)
        {
            _connect = connecting.GetDatebaseConnection();
            _connect.CreateTable<MapMarkerEntity>();
        }

        public void Insert(MapMarkerEntity marker)
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
            _connect.Table<MapMarkerEntity>().Where(x => x.TaskId == taskId);
        }

        public MapMarkerEntity GetById(int id)
        {
            return _connect.Get<MapMarkerEntity>(id);
        }

        public void Update(MapMarkerEntity entity)
        {
            _connect.Update(entity);
        }

        public void Delete(MapMarkerEntity entity)
        {
            _connect.Delete(entity);
        }

        public void Dispose()
        {
            
        }
    }
}
