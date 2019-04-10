using FirstApp.Core.Interfaces;
using FirstApp.Core.Models;
using MvvmCross;
using SQLite;
using System.Collections.Generic;
using System.Linq;

namespace FirstApp.Core.Services
{
    public class DBTaskService : IDBTaskService
    {
        private SQLiteConnection _connect;
        public DBTaskService(IDBConnectionService connecting)
        {        
            _connect = connecting.GetDatebaseConnection();
            _connect.CreateTable<TaskModel>();
        }

        public void AddTaskToTable(TaskModel task1)
        {
            if (task1.Id == 0)
            {
                _connect.Insert(task1);
            }
            else
            {
                _connect.Update(task1);
            }
        }

        public void DeleteTaskFromTable(TaskModel _task)
        {
            if (_task.Id != 0)
                _connect.Table<TaskModel>().Where(x => x.Id == _task.Id).Delete();
        }

        public List<TaskModel> LoadListItemsTask()
        {
            List<TaskModel> ListFromDatabase = (from i in _connect.Table<TaskModel>() select i).ToList();
            return ListFromDatabase;
        }

        public void UpdateLocalDatabese(List<TaskModel> items)
        {
            _connect.DropTable<TaskModel>();
            _connect.CreateTable<TaskModel>();
            for (int i = 0; i < items.Count; i++)
            {
                _connect.Insert(items[i]);
            }
        }
    }
}
