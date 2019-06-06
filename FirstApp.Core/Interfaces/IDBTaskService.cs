using FirstApp.Core.Entities;
using System.Collections.Generic;

namespace FirstApp.Core.Interfaces
{
    public interface IDBTaskService
    {
        void AddTaskToTable(TaskModel tasks);
        List<TaskModel> GetListAllTasks();
        void DeleteTaskFromTable(int taskId);
        List<TaskModel> LoadListItemsTask(int userId);
    }
}
