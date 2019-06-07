using FirstApp.Core.Entities;
using System.Collections.Generic;

namespace FirstApp.Core.Interfaces
{
    public interface ITaskService
    {
        void AddTaskToTable(TaskEntity tasks);
        List<TaskEntity> GetListAllTasks();
        void DeleteTaskFromDB(int taskId);
        List<TaskEntity> LoadListItemsTask(int userId);
    }
}
