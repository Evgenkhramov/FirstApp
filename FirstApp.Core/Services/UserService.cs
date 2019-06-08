using FirstApp.Core.Entities;
using FirstApp.Core.Interfaces;
using System.Collections;

namespace FirstApp.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepositoryService _userRepositoryService;
        private ISHA256hashService _sHA256HashService;

        public UserService(ISHA256hashService sHA256HashService,
            IUserRepositoryService userRepositoryService)
        {
            _userRepositoryService = userRepositoryService;
            _sHA256HashService = sHA256HashService;
        }

        public bool IsUserRegistrated(string email, string password)
        {
            byte[] bytePassword = _sHA256HashService.GetSHAFromString(password);

            UserDatabaseEntity findUser = _userRepositoryService.GetUserByEmail(password);

            return findUser != null && ByteArrayCompare(bytePassword, findUser.Password);
        }

        public bool ByteArrayCompare(byte[] byteArrayOne, byte[] byteArrayTwo)
        {
            IStructuralEquatable equalsArray = byteArrayOne;

            return equalsArray.Equals(byteArrayTwo, StructuralComparisons.StructuralEqualityComparer);
        }

        public int GetUserId(string email)
        {
            int findUserId = _userRepositoryService.GetUserIdByEmail(email);

            return findUserId;
        }

        public bool CheckEmailInDB(string email)
        {
            return _userRepositoryService.CheckEmailInDB(email);

        }

        public UserDatabaseEntity GetItem(int id)
        {
            return _userRepositoryService.GetItem(id);
        }

        public int DeleteItem(int id)
        {
            return _userRepositoryService.DeleteItem(id);
        }

        public int SaveItem(UserDatabaseEntity item)
        {
            if (item.Id != default(int))
            {
                _userRepositoryService.UpdateItem(item);
                return item.Id;
            }
            _userRepositoryService.InsertItem(item);

            return item.Id;
        }
    }
}

