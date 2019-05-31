using FirstApp.Core.Interfaces;
using FirstApp.Core.Models;
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

        public void AddTaskToTable(TaskModel task)
        {
            if (task.Id == 0)
            {
                _connect.Insert(task);
                return;
            }
            _connect.Update(task);
        }

        public void DeleteTaskFromTable(int taskId)
        {
            if (taskId >= 0)
            {
                _connect.Delete<TaskModel>(taskId);
            }
        }

        public List<TaskModel> LoadListAllTasks()
        {
            List<TaskModel> ListFromDatabase = (from item in _connect.Table<TaskModel>() select item).ToList();

            return ListFromDatabase;
        }

        public List<TaskModel> LoadListItemsTask(int userId)
        {
            List<TaskModel> ListFromDatabase = _connect.Query<TaskModel>($"SELECT * FROM Tasks WHERE UserId = {userId}");

            return ListFromDatabase;
        }
    }
}
