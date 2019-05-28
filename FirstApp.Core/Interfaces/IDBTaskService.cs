using FirstApp.Core.Models;
using System.Collections.Generic;

namespace FirstApp.Core.Interfaces
{
    public interface IDBTaskService
    {
        void AddTaskToTable(TaskModel tasks);
        List<TaskModel> LoadListItemsTask();
        void DeleteTaskFromTable(int taskId);
    }
}
