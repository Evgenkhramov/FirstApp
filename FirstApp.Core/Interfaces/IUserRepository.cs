using FirstApp.Core.Entities;

namespace FirstApp.Core.Interfaces
{
    public interface IUserRepository
    {
        UserDatabaseEntity GetUserByEmail(string email);
        int GetUserIdByEmail(string email);
        bool CheckEmailInDB(string email);
        UserDatabaseEntity GetItem(int id);
        int DeleteItem(int id);
        void UpdateItem(UserDatabaseEntity item);
        void InsertItem(UserDatabaseEntity item);
    }
}
