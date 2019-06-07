using FirstApp.Core.Entities;
using FirstApp.Core.Interfaces;
using SQLite;
using System.Collections.Generic;

namespace FirstApp.Core.Services
{
    public class FileNameService : IFileNameService
    {
        private readonly SQLiteConnection _connect;

        public FileNameService(IConnectionService connecting)
        {
            _connect = connecting.GetDatebaseConnection();

            _connect.CreateTable<FileListEntity>();
        }

        public void AddFileNameToTable(FileListEntity fileName)
        {
            _connect.Insert(fileName);
        }

        public List<FileListEntity> GetFileNameListFromDB(int taskId)
        {
            List<FileListEntity> list = _connect.Table<FileListEntity>().Where(x => x.TaskId == taskId).ToList();

            return list;
        }

        public void DeleteFiles(int taskId)
        {
            _connect.Query<FileListEntity>($"DELETE FROM FileName WHERE TaskId = {taskId}");
        }

        public void DeleteFileName(int fileId)
        {
            _connect.Delete<FileListEntity>(fileId);
        }
    }
}
