using FirstApp.Core.Entities;
using FirstApp.Core.Interfaces;
using System.Linq;

namespace FirstApp.Core.Repository
{
    public class SQLiteUserRepository : BaseRepository<UserDatabaseEntity>, IUserRepository
    {
        public SQLiteUserRepository(IConnectionService connect) : base(connect)
        {
      
        }

        public UserDatabaseEntity GetUserByEmail(string email)
        {
            UserDatabaseEntity findUser = _table.FirstOrDefault(x => x.Email == email);
            return findUser;
        }

        public int GetUserIdByEmail(string email)
        {
            int findUserId = _table.FirstOrDefault(x => x.Email == email).Id;

            return findUserId;
        }

        public bool CheckEmailInDB(string email)
        {
            bool isEmail = _table.Any(x => x.Email == email);

            return isEmail;
        }

        public void Delete(int id)
        {
           _connect.Delete<UserDatabaseEntity>(id);
        }
    }
}

