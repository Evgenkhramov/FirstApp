﻿using FirstApp.Core.Entities;
using FirstApp.Core.Interfaces;
using SQLite;
using System.Collections.Generic;

namespace FirstApp.Core.Repository
{
    class SQLiteTaskRepository : ITaskRepository
    {
        private readonly SQLiteConnection _connect;

        public SQLiteTaskRepository(IConnectionService connecting)
        {
            _connect = connecting.GetDatebaseConnection();
            _connect.CreateTable<TaskEntity>();
        }

        public void InsertTask(TaskEntity task)
        {
            _connect.Insert(task);
        }

        public void UpdateTask(TaskEntity task)
        {
            _connect.Update(task);
        }

        public void DeleteTask(int taskId)
        {
            _connect.Delete<TaskEntity>(taskId);
        }

        public List<TaskEntity> GetAllTasks()
        {
            List<TaskEntity> taskList = _connect.Table<TaskEntity>().ToList();

            return taskList;
        }

        public List<TaskEntity> GetUserTasks(int userId)
        {
            List<TaskEntity> taskList = _connect.Table<TaskEntity>().Where(x => x.UserId == userId).ToList();

            return taskList;
        }
    }
}