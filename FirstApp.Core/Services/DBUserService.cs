using FirstApp.Core.Interfaces;
using FirstApp.Core.Models;
using SQLite;
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
            return (from item in _connect.Table<UserDatabaseModel>() select item).ToList();
        }

        public bool IsUserRegistrated(string email, string password)
        {
            List<UserDatabaseModel> list = _connect.Query<UserDatabaseModel>($"SELECT * FROM Users WHERE Email = {email} AND Password = {password}");
            if (list.Count > 0)
            {
                return true;
            }
            return false;
        }

        public int GetUserId(string email)
        {
            List<UserDatabaseModel> list = _connect.Query<UserDatabaseModel>($"SELECT * FROM Users WHERE Email = {email}");
            int userId = list[0].Id;
            return userId;
        }

        public bool IsEmailInDB(string email)
        {
            UserDatabaseModel findUser = _connect.Table<UserDatabaseModel>().FirstOrDefault(x => x.Email == email);

            if (findUser != null)
            {
                return true;
            }

            return false;
        }

        public UserDatabaseModel GetItem(int id)
        {
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

            return _connect.Insert(item);
        }
    }
}

