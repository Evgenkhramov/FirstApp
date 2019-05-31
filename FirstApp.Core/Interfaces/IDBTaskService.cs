using FirstApp.Core.Models;
using System.Collections.Generic;

namespace FirstApp.Core.Interfaces
{
    public interface IDBTaskService
    {
        void AddTaskToTable(TaskModel tasks);
        List<TaskModel> LoadListAllTasks();
        void DeleteTaskFromTable(int taskId);
        List<TaskModel> LoadListItemsTask(int userId);
    }
}
