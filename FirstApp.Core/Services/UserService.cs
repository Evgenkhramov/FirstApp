using FirstApp.Core.Entities;
using FirstApp.Core.Interfaces;
using SQLite;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FirstApp.Core.Services
{
    public class UserService : IUserService
    {

        private SQLiteConnection _connecting;
        private ISHA256hashService _sHA256HashService;

        public UserService(IConnectionService connect, ISHA256hashService sHA256HashService)
        {
            _sHA256HashService = sHA256HashService;
            _connecting = connect.GetDatebaseConnection();

            _connecting.CreateTable<UserDatabaseEntity>();
        }

        public IEnumerable<UserDatabaseEntity> GetItems()
        {
            return _connecting.Table<UserDatabaseEntity>().ToList(); 
        }

        public bool IsUserRegistrated(string email, string password)
        {
            byte[] bytePassword = _sHA256HashService.GetSHAFromString(password);

            UserDatabaseEntity findUser = _connecting.Table<UserDatabaseEntity>().FirstOrDefault(x => x.Email == email);

            return findUser != null && ByteArrayCompare(bytePassword, findUser.Password);
        }

        public bool ByteArrayCompare(byte[] byteArrayOne, byte[] byteArrayTwo)
        {
            IStructuralEquatable equalsArray = byteArrayOne;

            return equalsArray.Equals(byteArrayTwo, StructuralComparisons.StructuralEqualityComparer);
        }

        public int GetUserId(string email)
        {
            int findUserId = _connecting.Table<UserDatabaseEntity>().FirstOrDefault(x => x.Email == email).Id;  
 
            return findUserId;
        }

        public bool IsEmailInDB(string email)
        {
            bool isEmail = _connecting.Table<UserDatabaseEntity>().Any(x => x.Email == email);

            return isEmail;
        }

        public UserDatabaseEntity GetItem(int id)
        {
            return _connecting.Get<UserDatabaseEntity>(id);
        }

        public int DeleteItem(int id)
        {
            return _connecting.Delete<UserDatabaseEntity>(id);
        }

        public int SaveItem(UserDatabaseEntity item)
        {
            if (item.Id != default(int))
            {
                _connecting.Update(item);
                return item.Id;
            }
            _connecting.Insert(item);

            return item.Id;
        }
    }
}

