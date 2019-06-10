using FirstApp.Core.Entities;
using FirstApp.Core.Interfaces;
using SQLite;
using System.Collections.Generic;

namespace FirstApp.Core.Repository
{
    public class SQLiteFileRepository : IFileNameRepository, IBaseRepository<FileListEntity>
    {
        private readonly SQLiteConnection _connect;

        public SQLiteFileRepository(IConnectionService connecting)
        {
            _connect = connecting.GetDatebaseConnection();

            _connect.CreateTable<FileListEntity>();
        }

        public void Insert(FileListEntity fileName)
        {
            _connect.Insert(fileName);
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

        public void Update(FileListEntity entity)
        {
            _connect.Update(entity);
        }

        public void Delete(FileListEntity entity)
        {
            _connect.Delete(entity);
        }

        FileListEntity IBaseRepository<FileListEntity>.GetById(int id)
        {
            return _connect.Table<FileListEntity>().Where(i => i.Id == id).FirstOrDefault();
        }

        public void Dispose()
        {

        }
    }
}
