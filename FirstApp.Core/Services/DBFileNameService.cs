using FirstApp.Core.Interfaces;
using FirstApp.Core.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

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

        public string[] GetFileNameFromDB(int taskId)
        {
            string[] names = null;
            var list = _connect.Query<FileListModel>($"SELECT * FROM FileName WHERE TaskId = {taskId}");
            for (int i = 0; i < list.Capacity; i++)
            {
                names[i] = list[i].FileName;
            }
            return names;
        }

        public void DeleteFileName(int id)
        {
            _connect.Delete<FileListModel>(id);
        }
    }
}
