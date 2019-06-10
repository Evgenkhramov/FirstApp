using Acr.UserDialogs;
using FirstApp.Core.Interfaces;
using FirstApp.Core.Models;
using FirstApp.Core.Providers;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System.Threading.Tasks;

namespace FirstApp.Core.ViewModels
{
    public class LoginViewModel : BaseViewModel, IFacebookAuthenticationDelegate
    {
        #region Variables

        private readonly IAuthorizationService _authorizationService;
        private readonly IRegistrationService _registrationService;
        private readonly IFacebookService _facebookService;

        #endregion Variables

        #region Constructors
        public LoginViewModel(IAuthorizationService authorizationService, IRegistrationService registrationService, IFacebookService facebookService, 
            IMvxNavigationService navigationService, IUserDialogs userDialogs) : base(navigationService, userDialogs)
        {
            ShowMainViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<MainViewModel>());

            _authorizationService = authorizationService;
            _registrationService = registrationService;
            _facebookService = facebookService;

            HaveGone = true;
            SaveButton = true;
        }

        #endregion Constructors

        #region Properties
        public bool HaveGone { get; set; }

        public bool SaveButton { get; set; }

        private string _userEmail;
        public string UserEmail
        {
            get => _userEmail;
            set
            {
                _userEmail = value;
            }
        }

        private string _userPassword;
        public string UserPassword
        {
            get => _userPassword;
            set
            {
                _userPassword = value;
            }
        }
        #endregion Properties

        #region Commands  
        public IMvxAsyncCommand ShowMainViewModelCommand { get; private set; }

        public MvxAsyncCommand UserLogin
        {
            get
            {
                return new MvxAsyncCommand(async () =>
                {
                    if (_authorizationService.IsLoggedIn(UserEmail, UserPassword))
                    {
                        await _navigationService.Navigate<MainViewModel>();

                        return;
                    }
                    _userDialogs.Alert(Constants.InvalidUserNameOrPassword);
                });
            }
        }

        public MvxAsyncCommand UserRegistration
        {
            get
            {
                return new MvxAsyncCommand(async () =>
                {
                    await _navigationService.Navigate<RegistrationViewModel>();
                });
            }
        }
        #endregion Commands

        #region Methods
        public async Task OnAuthenticationCompleted(FacebookOAuthToken token)
        {
            FacebookUserModel user = await _facebookService.GetUserDataAsync(token.AccessToken);

            if (user == null)
            {
                _userDialogs.Alert(Constants.DidNotComplite);
                return;
            }

            string userPhoto = await _facebookService.GetImageFromUrlToBase64(user.UserPicture.PictureData.Url);

            int userId = _registrationService.SaveUserSocialNetworks(user.FirstName, user.Email, user.Id, user.LastName,
                user.UserPicture.PictureData.Url, userPhoto, LoginType.Facebook);

            _registrationService.UserRegistration(userId.ToString());

            await _navigationService.Navigate<MainViewModel>();
        }

        public async Task OnGoogleAuthenticationCompleted(GoogleUserModel user)
        {
            if (user == null)
            {
                _userDialogs.Alert(Constants.DidNotComplite);
                return;
            }

            string userPhoto = await _facebookService.GetImageFromUrlToBase64(user.Picture);

            int userId = _registrationService.SaveUserSocialNetworks(user.First_name, user.Email, user.Id, user.Last_name, user.Picture, userPhoto, LoginType.Google);

            _registrationService.UserRegistration(userId.ToString());

            await _navigationService.Navigate<MainViewModel>();
        }

        public async Task OnAuthenticationCanceled()
        {
            await _userDialogs.AlertAsync(Constants.DidNotComplite);
        }

        public async Task OnAuthenticationFailed()
        {
            await _userDialogs.AlertAsync(Constants.DidNotComplite);
        }

        public async Task SaveUserGoogleiOS(GoogleUserModeliOS user)
        {
            if (user == null)
            {
                _userDialogs.Alert(Constants.DidNotComplite);
                return;
            }

            string userPhoto = await _facebookService.GetImageFromUrlToBase64(user.UserImage.Url);

            int userId = _registrationService.SaveUserSocialNetworks(user.UserName.GivenName, user.Emails[0].Value, 0,
                user.UserName.FamilyName, user.UserImage.Url, userPhoto, LoginType.Google);

            _registrationService.UserRegistration(userId.ToString());

            await _navigationService.Navigate<MainViewModel>();
        }
        #endregion Methods      
    }
}
