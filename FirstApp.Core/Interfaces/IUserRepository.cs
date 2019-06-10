using FirstApp.Core.Entities;

namespace FirstApp.Core.Interfaces
{
    public interface IUserRepository : IBaseRepository<UserDatabaseEntity>
    {
        UserDatabaseEntity GetUserByEmail(string email);
        int GetUserIdByEmail(string email);
        bool CheckEmailInDB(string email);
        void Delete(int id);
    }
}
