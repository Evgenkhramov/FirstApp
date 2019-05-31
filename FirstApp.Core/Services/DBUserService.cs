﻿using FirstApp.Core.Interfaces;
using FirstApp.Core.Models;
using SQLite;
using System.Collections.Generic;
using System.Linq;

namespace FirstApp.Core.Services
{
    public class DBUserService : IDBUserService
    {

        private SQLiteConnection _connecting;
        private ISHA256hashService _sHA256HashService;

        public DBUserService(IDBConnectionService connect, ISHA256hashService sHA256HashService)
        {
            _sHA256HashService = sHA256HashService;
            _connecting = connect.GetDatebaseConnection();
            _connecting.CreateTable<UserDatabaseModel>();
        }

        public IEnumerable<UserDatabaseModel> GetItems()
        {
            return (from item in _connecting.Table<UserDatabaseModel>() select item).ToList();
        }

        public bool IsUserRegistrated(string email, string password)
        {
            byte[] bytePassword = _sHA256HashService.GetSHAFromString(password);
            List<UserDatabaseModel> list = _connecting.Query<UserDatabaseModel>($"SELECT * FROM Users WHERE Email = {email} AND Password = {bytePassword}");
            if (list.Count > 0)
            {
                return true;
            }
            return false;
        }

        public int GetUserId(string email)
        {
            List<UserDatabaseModel> list = _connecting.Query<UserDatabaseModel>($"SELECT * FROM Users WHERE Email = {email}");
            int userId = list[0].Id;
            return userId;
        }

        public bool IsEmailInDB(string email)
        {
            UserDatabaseModel findUser = _connecting.Table<UserDatabaseModel>().FirstOrDefault(x => x.Email == email);

            if (findUser != null)
            {
                return true;
            }

            return false;
        }

        public UserDatabaseModel GetItem(int id)
        {
            return _connecting.Get<UserDatabaseModel>(id);
        }

        public int DeleteItem(int id)
        {
            return _connecting.Delete<UserDatabaseModel>(id);
        }

        public int SaveItem(UserDatabaseModel item)
        {
            if (item.Id != 0)
            {
                _connecting.Update(item);
                return item.Id;
            }

            return _connecting.Insert(item);
        }
    }
}

