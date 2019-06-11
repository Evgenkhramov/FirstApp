using FirstApp.Core.Entities;
using FirstApp.Core.Interfaces;
using System.Collections.Generic;

namespace FirstApp.Core.Repository
{
    public class SQLiteMapMarkersRepository : BaseRepository<MapMarkerEntity>, IMarkersRepository
    {
        public SQLiteMapMarkersRepository(IConnectionService connecting) : base(connecting)
        {
        }

        public void InsertMarkers(List<MapMarkerEntity> list)
        {
            _connect.InsertAll(list);
        }

        public List<MapMarkerEntity> GetMarkersList(int taskId)
        {
            List<MapMarkerEntity> list = _table.Where(x => x.TaskId == taskId).ToList();

            return list;
        }

        public void DeleteMarkers(int taskId)
        {
            _table.Where(x => x.TaskId == taskId);
        }
    }
}
