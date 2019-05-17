using Plugin.SecureStorage;
using FirstApp.Core.Interfaces;

namespace FirstApp.Core.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        public bool IsLoggedIn(string userName, string userPassword)
        {
            if (CrossSecureStorage.Current.HasKey(Constants.SequreKeyForUserName) && CrossSecureStorage.Current.GetValue(Constants.SequreKeyForUserName) == userName
                && CrossSecureStorage.Current.GetValue(Constants.SequreKeyForUserPassword) == userPassword)
            {
                CrossSecureStorage.Current.SetValue(Constants.SequreKeyForLoged, "true");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
