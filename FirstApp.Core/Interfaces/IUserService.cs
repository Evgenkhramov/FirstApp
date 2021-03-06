﻿using FirstApp.Core.Entities;

namespace FirstApp.Core.Interfaces
{
    public interface IUserService
    {
        UserEntity GetItem(int id);
        void DeleteItem(int id);
        int SaveItem(UserEntity item);
        bool CheckEmailInDB(string login);
        bool IsUserRegistrated(string email, string password);
        int GetUserId(string email);
        bool CompareByteArrays(byte[] firstArray, byte[] secondArray);
    }
}
