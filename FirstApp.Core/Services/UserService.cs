﻿using FirstApp.Core.Entities;
using FirstApp.Core.Interfaces;
using System.Collections;

namespace FirstApp.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private ISHA256hashService _sHA256HashService;

        public UserService(ISHA256hashService sHA256HashService, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _sHA256HashService = sHA256HashService;
        }

        public bool IsUserRegistrated(string email, string password)
        {
            byte[] bytePassword = _sHA256HashService.GetSHAFromString(password);

            UserEntity findUser = _userRepository.GetUserByEmail(password);

            return findUser != null && CompareByteArrays(bytePassword, findUser.Password);
        }

        public bool CompareByteArrays(byte[] byteArrayOne, byte[] byteArrayTwo)
        {
            IStructuralEquatable equalsArray = byteArrayOne;

            return equalsArray.Equals(byteArrayTwo, StructuralComparisons.StructuralEqualityComparer);
        }

        public int GetUserId(string email)
        {
            int findUserId = _userRepository.GetUserIdByEmail(email);

            return findUserId;
        }

        public bool CheckEmailInDB(string email)
        {
            return _userRepository.CheckEmailInDB(email);

        }

        public UserEntity GetItem(int id)
        {
            return _userRepository.GetById(id);
        }

        public void DeleteItem(int id)
        {
            _userRepository.Delete(id);
        }

        public int SaveItem(UserEntity item)
        {
            if (item.Id != default(int))
            {
                _userRepository.Update(item);
                return item.Id;
            }

            _userRepository.Insert(item);

            return item.Id;
        }
    }
}

