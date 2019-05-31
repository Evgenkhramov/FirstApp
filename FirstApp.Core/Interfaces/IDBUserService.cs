using FirstApp.Core.Models;
using System.Collections.Generic;

namespace FirstApp.Core.Interfaces
{
    public interface IDBUserService
    {
        IEnumerable<UserDatabaseModel> GetItems();
        UserDatabaseModel GetItem(int id);
        int DeleteItem(int id);
        int SaveItem(UserDatabaseModel item);
        bool IsEmailInDB(string login);
        bool IsUserRegistrated(string email, string password);
        int GetUserId(string email);
        bool ByteArrayCompare(byte[] a1, byte[] a2);
    }
}
