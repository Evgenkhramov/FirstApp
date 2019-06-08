using FirstApp.Core.Entities;
using FirstApp.Core.Interfaces;
using SQLite;
using System.Linq;

namespace FirstApp.Core.Repository
{
    public class SQLiteUserRepositoryService : IUserRepositoryService
    {
        private SQLiteConnection _connecting;

        public SQLiteUserRepositoryService(IConnectionService connect)
        {
            _connecting = connect.GetDatebaseConnection();

            _connecting.CreateTable<UserDatabaseEntity>();
        }

        public UserDatabaseEntity GetUserByEmail(string email)
        {
            UserDatabaseEntity findUser = _connecting.Table<UserDatabaseEntity>().FirstOrDefault(x => x.Email == email);
            return findUser;
        }

        public int GetUserIdByEmail(string email)
        {
            int findUserId = _connecting.Table<UserDatabaseEntity>().FirstOrDefault(x => x.Email == email).Id;

            return findUserId;
        }

        public bool CheckEmailInDB(string email)
        {
            bool isEmail = _connecting.Table<UserDatabaseEntity>().Any(x => x.Email == email);

            return isEmail;
        }

        public UserDatabaseEntity GetItem(int id)
        {
            UserDatabaseEntity user = new UserDatabaseEntity(); 
            return user = _connecting.Get<UserDatabaseEntity>(id);
        }

        public int DeleteItem(int id)
        {
            return _connecting.Delete<UserDatabaseEntity>(id);
        }

        public void UpdateItem(UserDatabaseEntity item)
        {
            _connecting.Update(item);
        }

        public void InsertItem(UserDatabaseEntity item)
        {
            _connecting.Insert(item);
        }
    }
}

