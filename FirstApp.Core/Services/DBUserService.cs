using FirstApp.Core.Entities;
using FirstApp.Core.Interfaces;
using SQLite;
using System.Collections;
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

            UserDatabaseModel findUser = _connecting.Table<UserDatabaseModel>().FirstOrDefault(x => x.Email == email);

            if (findUser != null && ByteArrayCompare(bytePassword, findUser.Password))
            {
                return true;
            }

            return false;
        }

        public bool ByteArrayCompare(byte[] byteArrayOne, byte[] byteArrayTwo)
        {
            IStructuralEquatable equalsArray = byteArrayOne;

            return equalsArray.Equals(byteArrayTwo, StructuralComparisons.StructuralEqualityComparer);
        }

        public int GetUserId(string email)
        {
            int findUserId = _connecting.Table<UserDatabaseModel>().FirstOrDefault(x => x.Email == email).Id;  
 
            return findUserId;
        }

        public bool IsEmailInDB(string email)
        {
            bool isEmail = _connecting.Table<UserDatabaseModel>().Any(x => x.Email == email);

            return isEmail;
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
            _connecting.Insert(item);

            return item.Id;
        }
    }
}

