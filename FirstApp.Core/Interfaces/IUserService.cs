using FirstApp.Core.Entities;
using System.Collections.Generic;

namespace FirstApp.Core.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserDatabaseEntity> GetItems();
        UserDatabaseEntity GetItem(int id);
        int DeleteItem(int id);
        int SaveItem(UserDatabaseEntity item);
        bool IsEmailInDB(string login);
        bool IsUserRegistrated(string email, string password);
        int GetUserId(string email);
        bool ByteArrayCompare(byte[] a1, byte[] a2);
    }
}
