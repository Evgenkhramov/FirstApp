using FirstApp.Core.Entities;
using FirstApp.Core.Interfaces;
using SQLite;
using System.Collections.Generic;

namespace FirstApp.Core.Repository
{
    public class SQLiteFileRepository : BaseRepository<FileListEntity>, IFileNameRepository
    {
        private readonly SQLiteConnection _connect;

        public SQLiteFileRepository(IConnectionService connecting):base(connecting)
        {
            _connect = connecting.GetDatebaseConnection();

            _connect.CreateTable<FileListEntity>();
        }

        public List<FileListEntity> Get(int taskId)
        {
            List<FileListEntity> list = _connect.Table<FileListEntity>().Where(x => x.TaskId == taskId).ToList();

            return list;
        }

        public void DeleteFiles(int taskId)
        {
            _connect.Table<FileListEntity>().Where(x => x.TaskId == taskId).Delete();
        }

        public void Delete(int fileId)
        {
            _connect.Delete<FileListEntity>(fileId);
        }

        FileListEntity IBaseRepository<FileListEntity>.GetById(int id)
        {
            return _connect.Table<FileListEntity>().Where(i => i.Id == id).FirstOrDefault();
        }
    }
}
