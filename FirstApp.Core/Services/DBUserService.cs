using FirstApp.Core.Interfaces;
using MvvmCross;
using SQLite;
using System;
using FirstApp.Core;
using FirstApp.Core.Models;
using System.Collections.Generic;
using System.Linq;


namespace FirstApp.Core.Services
{
    public class DBUserService : IDBUserService
    {
        private SQLiteConnection _connect;
        public DBUserService(IDBConnectionService connect)
        {
            _connect = connect.GetDatebaseConnection();
            _connect.CreateTable<UserDatabaseModel>();
        }

        public IEnumerable<UserDatabaseModel> GetItems()
        {
            return (from i in _connect.Table<UserDatabaseModel>() select i).ToList();
        }

        public bool IsLoginInDB(string login)
        {
            UserDatabaseModel findUser = _connect.Table<UserDatabaseModel>().FirstOrDefault(x => x.Name == login);
            if (findUser == null)
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }

        public UserDatabaseModel GetItem(int id)
        {
            //return database.Table<UserDatabaseModel>().ToList();
           return _connect.Get<UserDatabaseModel>(id);
        }
        public int DeleteItem(int id)
        {
            return _connect.Delete<UserDatabaseModel>(id);
        }
        public int SaveItem(UserDatabaseModel item)
        {
            if (item.Id != 0)
            {
                _connect.Update(item);
                return item.Id;
            }
            else
            {
                return _connect.Insert(item);
            }
        }
    }
}

