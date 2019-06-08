using FirstApp.Core.Entities;
using System.Collections.Generic;

namespace FirstApp.Core.Interfaces
{
    public interface IUserService
    {
        UserDatabaseEntity GetItem(int id);
        int DeleteItem(int id);
        int SaveItem(UserDatabaseEntity item);
        bool CheckEmailInDB(string login);
        bool IsUserRegistrated(string email, string password);
        int GetUserId(string email);
        bool ByteArrayCompare(byte[] a1, byte[] a2);
    }
}
