using FirstApp.Core.Entities;
using FirstApp.Core.Interfaces;
using SQLite;
using System.Collections.Generic;

namespace FirstApp.Core.Services
{
    public class DBFileNameService : IDBFileNameService
    {
        private SQLiteConnection _connect;

        public DBFileNameService(IDBConnectionService connecting)
        {
            _connect = connecting.GetDatebaseConnection();

            _connect.CreateTable<FileListModel>();
        }

        public void AddFileNameToTable(FileListModel fileName)
        {
            _connect.Insert(fileName);
        }

        public List<FileListModel> GetFileNameListFromDB(int taskId)
        {
            List<FileListModel> list = _connect.Table<FileListModel>().Where(x => x.TaskId == taskId).ToList();

            return list;
        }

        public void DeleteFiles(int taskId)
        {
            _connect.Query<FileListModel>($"DELETE FROM FileName WHERE TaskId = {taskId}");
        }

        public void DeleteFileName(int fileId)
        {
            _connect.Delete<FileListModel>(fileId);
        }
    }
}
