using FirstApp.Core.Entities;
using FirstApp.Core.Interfaces;
using System.Collections.Generic;

namespace FirstApp.Core.Repository
{
    class SQLiteTaskRepository : BaseRepository<TaskEntity>, ITaskRepository
    {
        public SQLiteTaskRepository(IConnectionService connecting) : base(connecting)
        {

        }

        public void DeleteTask(int taskId)
        {
            _connect.Delete<TaskEntity>(taskId);
        }

        public List<TaskEntity> GetAllTasks()
        {
            List<TaskEntity> taskList = _table.ToList();

            return taskList;
        }

        public List<TaskEntity> GetUserTasks(int userId)
        {
            List<TaskEntity> taskList = _table.Where(x => x.UserId == userId).ToList();

            return taskList;
        }
    }
}
