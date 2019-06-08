using FirstApp.Core.Entities;
using FirstApp.Core.Interfaces;
using System.Collections.Generic;

namespace FirstApp.Core.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepositoryService _taskRepository;

        public TaskService(ITaskRepositoryService taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public void AddTaskToTable(TaskEntity task)
        {
            if (task.Id == default(int))
            {
                _taskRepository.InsertTask(task);

                return;
            }
            _taskRepository.UpdateTask(task);
        }

        public void DeleteTaskFromDB(int taskId)
        {
            if (taskId >= default(int))
            {
                _taskRepository.DeleteTask(taskId);
            }
        }

        public List<TaskEntity> GetListAllTasks()
        {
            return _taskRepository.GetAllTasks();
        }

        public List<TaskEntity> LoadListItemsTask(int userId)
        {
            return _taskRepository.GetUserTasks(userId);
        }
    }
}
