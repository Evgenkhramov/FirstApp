using FirstApp.Core.Entities;
using System.Collections.Generic;

namespace FirstApp.Core.Interfaces
{
    public interface ITaskRepository : IBaseRepository<TaskEntity>
    {
        void DeleteTask(int taskId);
        List<TaskEntity> GetAllTasks();
        List<TaskEntity> GetUserTasks(int userId);
    }
}
