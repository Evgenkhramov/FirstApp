using FirstApp.Core.Interfaces;
using FirstApp.Core.Models;
using Plugin.SecureStorage;

namespace FirstApp.Core.Services
{
    class RegistrationService : IRegistrationService
    {
        private IDBUserService _dBUserService;
        private ISHA256hashService _sHA256Hash;

        public RegistrationService(IDBUserService dBUserService, ISHA256hashService sHA256Hash)
        {
            _sHA256Hash = sHA256Hash;
            _dBUserService = dBUserService;
        }

        public void UserRegistration(string id)
        {
            CrossSecureStorage.Current.SetValue(Constants.SequreKeyForUserIdInDB, id);
            CrossSecureStorage.Current.SetValue(Constants.SequreKeyForLoged, Constants.LogIn);
        }

        public int SaveUserInDbFromApp(string registrationUserName, string registrationUserPassword, string userEmail, LoginType loginType)
        {
            var userDatabaseModel = new UserDatabaseModel
            {
                Name = registrationUserName,
                Password = _sHA256Hash.GetSHAFromString(registrationUserPassword),
                Email = userEmail,
                TypeUserLogin = LoginType.App
            };

            _dBUserService.SaveItem(userDatabaseModel);
            int userId = userDatabaseModel.Id;
            return userId;
        }

        public int SaveUserInDbFromSocialNetworks(string registrationUserName, string userEmail, double userIdFromSocialNetworks,
            string surname, string photoUrl, string userPhoto, LoginType loginType)
        {
            var userDatabaseModel = new UserDatabaseModel
            {
                Name = registrationUserName,
                Surname = surname,
                UserId = userIdFromSocialNetworks,
                Email = userEmail,
                PhotoURL = photoUrl,
                Photo = userPhoto,
                TypeUserLogin = LoginType.App
            };

            _dBUserService.SaveItem(userDatabaseModel);
            int userId = userDatabaseModel.Id;
            return userId;
        }
    }
}
