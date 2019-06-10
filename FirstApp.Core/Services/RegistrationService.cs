using FirstApp.Core.Entities;
using FirstApp.Core.Interfaces;
using FirstApp.Core.Models;
using Plugin.SecureStorage;

namespace FirstApp.Core.Services
{
    class RegistrationService : IRegistrationService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISHA256hashService _sHA256Hash;

        public RegistrationService(IUserRepository userRepository, ISHA256hashService sHA256Hash)
        {
            _sHA256Hash = sHA256Hash;
            _userRepository = userRepository;
        }

        public void UserRegistration(string id)
        {
            CrossSecureStorage.Current.SetValue(Constants.SequreKeyForUserIdInDB, id);
            CrossSecureStorage.Current.SetValue(Constants.SequreKeyForLoged, Constants.LogIn);
        }

        public int SaveUserInDbFromApp(string registrationUserName, string registrationUserPassword, string userEmail, LoginType loginType)
        {
            var user = new UserDatabaseEntity
            {
                Name = registrationUserName,
                Password = _sHA256Hash.GetSHAFromString(registrationUserPassword),
                Email = userEmail,
                TypeUserLogin = LoginType.App
            };

            _userRepository.InsertItem(user);

            int userId = user.Id;

            return userId;
        }

        public int SaveUserSocialNetworks(string registrationUserName, string userEmail, double userIdFromSocialNetworks,
            string surname, string photoUrl, string userPhoto, LoginType loginType)
        {
            var userDatabaseModel = new UserDatabaseEntity
            {
                Name = registrationUserName,
                Surname = surname,
                UserId = userIdFromSocialNetworks,
                Email = userEmail,
                PhotoURL = photoUrl,
                Photo = userPhoto,
                TypeUserLogin = LoginType.App
            };

            _userRepository.InsertItem(userDatabaseModel);

            int userId = userDatabaseModel.Id;

            return userId;
        }
    }
}
