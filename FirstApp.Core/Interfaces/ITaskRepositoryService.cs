using FirstApp.Core.Entities;
using System.Collections.Generic;

namespace FirstApp.Core.Interfaces
{
    public interface ITaskRepositoryService
    {
        void InsertTask(TaskEntity task);
        void UpdateTask(TaskEntity task);
        void DeleteTask(int taskId);
        List<TaskEntity> GetAllTasks();
        List<TaskEntity> GetUserTasks(int userId);
    }
}
