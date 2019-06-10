using FirstApp.Core.Entities;
using FirstApp.Core.Interfaces;
using SQLite;
using System.Linq;

namespace FirstApp.Core.Repository
{
    public class SQLiteUserRepository : BaseRepository<UserDatabaseEntity>, IUserRepository
    {
        private SQLiteConnection _connecting;

        public SQLiteUserRepository(IConnectionService connect) : base(connect)
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

        public void Delete(int id)
        {
           _connecting.Delete<UserDatabaseEntity>(id);
        }
    }
}

