using FirstApp.Core.Interfaces;
using Plugin.SecureStorage;

namespace FirstApp.Core.Services
{
    class RegistrationService : IRegistrationService
    {
        public void UserRegistration(string name, string password, string id)
        {
            CrossSecureStorage.Current.SetValue(Constants.SequreKeyForUserIdInDB, id);
            CrossSecureStorage.Current.SetValue(Constants.SequreKeyForUserName, name);
            CrossSecureStorage.Current.SetValue(Constants.SequreKeyForUserPassword, password);
            CrossSecureStorage.Current.SetValue(Constants.SequreKeyForLoged, Constants.LogIn);
        }
    }
}
