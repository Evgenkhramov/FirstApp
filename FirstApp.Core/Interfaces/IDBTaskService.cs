using FirstApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstApp.Core.Interfaces
{
    public interface IDBTaskService
    {
        void AddTaskToTable(TaskModel tasks);
        List<TaskModel> LoadListItemsTask();
        void DeleteTaskFromTable(int taskId);
        //void UpdateLocalDatabese(List<TaskModel> items);
    }
}
