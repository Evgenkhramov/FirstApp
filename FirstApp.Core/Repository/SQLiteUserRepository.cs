using FirstApp.Core.Entities;
using FirstApp.Core.Interfaces;
using System.Linq;

namespace FirstApp.Core.Repository
{
    public class SQLiteUserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        public SQLiteUserRepository(IConnectionService connect) : base(connect)
        {
      
        }

        public UserEntity GetUserByEmail(string email)
        {
            UserEntity findUser = _table.FirstOrDefault(x => x.Email == email);
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
           _connect.Delete<UserEntity>(id);
        }
    }
}

