using Plugin.SecureStorage;
using FirstApp.Core.Interfaces;


namespace FirstApp.Core.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUserService _userService;

        public AuthorizationService(IUserService userService)
        {
            _userService = userService;
        }

        public bool CheckLoggedIn(string userEmail, string userPassword)
        {
            if (!_userService.IsUserRegistrated(userEmail, userPassword))
            {
                return false;
            }

            int userId = _userService.GetUserId(userEmail);
            string userIdString = userId.ToString();

            CrossSecureStorage.Current.SetValue(Constants.SequreKeyForUserIdInDB, userIdString);
            CrossSecureStorage.Current.SetValue(Constants.SequreKeyForLoged, Constants.LogIn);

            return true;
        }
    }
}
