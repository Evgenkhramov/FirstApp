using Plugin.SecureStorage;
using FirstApp.Core.Interfaces;


namespace FirstApp.Core.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUserService _dBUserService;

        public AuthorizationService(IUserService dBUserService)
        {
            _dBUserService = dBUserService;
        }

        public bool IsLoggedIn(string userEmail, string userPassword)
        {
            if (!_dBUserService.IsUserRegistrated(userEmail, userPassword))
            {
                return false;
            }

            int userId = _dBUserService.GetUserId(userEmail);
            string userIdString = userId.ToString();

            CrossSecureStorage.Current.SetValue(Constants.SequreKeyForUserIdInDB, userIdString);
            CrossSecureStorage.Current.SetValue(Constants.SequreKeyForLoged, Constants.LogIn);

            return true;
        }
    }
}
