using FirstApp.Core.Entities;
using FirstApp.Core.Interfaces;
using System.Collections.Generic;

namespace FirstApp.Core.Repository
{
    public class SQLiteFileRepository : BaseRepository<FileListEntity>, IFileNameRepository
    {
        public SQLiteFileRepository(IConnectionService connecting):base(connecting)
        {
        }

        public List<FileListEntity> Get(int taskId)
        {
            List<FileListEntity> list = _table.Where(x => x.TaskId == taskId).ToList();

            return list;
        }

        public void DeleteFiles(int taskId)
        {
            _table.Where(x => x.TaskId == taskId).Delete();
        }

        public void Delete(int fileId)
        {
            _connect.Delete<FileListEntity>(fileId);
        }

        FileListEntity IBaseRepository<FileListEntity>.GetById(int id)
        {
            return _table.Where(i => i.Id == id).FirstOrDefault();
        }
    }
}
