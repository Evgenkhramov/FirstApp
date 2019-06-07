using FirstApp.Core.Entities;
using FirstApp.Core.Interfaces;
using SQLite;
using System.Collections.Generic;
using System.Linq;

namespace FirstApp.Core.Services
{
    public class TaskService : ITaskService
    {
        private readonly SQLiteConnection _connect;

        public TaskService(IConnectionService connecting)
        {
            _connect = connecting.GetDatebaseConnection();
            _connect.CreateTable<TaskEntity>();
        }

        public void AddTaskToTable(TaskEntity task)
        {
            if (task.Id == default(int))
            {
                _connect.Insert(task);

                return;
            }
            _connect.Update(task);
        }

        public void DeleteTaskFromDB(int taskId)
        {
            if (taskId >= default(int))
            {
                _connect.Delete<TaskEntity>(taskId);
            }
        }

        public List<TaskEntity> GetListAllTasks()
        {
            List<TaskEntity> listFromDatabase = _connect.Table<TaskEntity>().ToList(); 

            return listFromDatabase;
        }

        public List<TaskEntity> LoadListItemsTask(int userId)
        {
            List<TaskEntity> listFromDatabase = _connect.Table<TaskEntity>().Where(x => x.UserId == userId).ToList();

            return listFromDatabase;
        }
    }
}
