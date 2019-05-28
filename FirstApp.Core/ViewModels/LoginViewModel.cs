using Acr.UserDialogs;
using FirstApp.Core.Interfaces;
using FirstApp.Core.Models;
using FirstApp.Core.Providers;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Plugin.SecureStorage;
using System.Threading.Tasks;

namespace FirstApp.Core.ViewModels
{
    public class LoginViewModel : BaseViewModel, IFacebookAuthenticationDelegate
    {
        #region Variables
        private readonly IAuthorizationService _authorizationService;
        private readonly IRegistrationService _registrationService;
        private readonly IDBUserService _sqlLiteRepository;
        private readonly IFacebookService _facebookService;
        #endregion Variables

        #region Constructors
        public LoginViewModel(IAuthorizationService authorizationService, IDBUserService sQLiteRepository, IRegistrationService registrationService, IFacebookService facebookService, IMvxNavigationService navigationService, IUserDialogs userDialogs) : base(navigationService, userDialogs)
        {
            ShowMainViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<MainViewModel>());
            _authorizationService = authorizationService;
            _sqlLiteRepository = sQLiteRepository;
            _registrationService = registrationService;
            _facebookService = facebookService;
            HaveGone = true;
            SaveButton = true;
        }

        #endregion Constructors

        #region Properties
        public bool HaveGone { get; set; }

        public bool SaveButton { get; set; }

        private string _userName;
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
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
                    if (_authorizationService.IsLoggedIn(UserName, UserPassword))
                    {
                        CrossSecureStorage.Current.SetValue(Constants.SequreKeyForLoged, Constants.LogIn);
                        await _navigationService.Navigate<MainViewModel>();
                    }
                    if (!_authorizationService.IsLoggedIn(UserName, UserPassword))
                    {
                        _userDialogs.Alert(Constants.InvalidUserNameOrPassword);
                    }
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
            FacebookModel user = await _facebookService.GetUserDataAsync(token.AccessToken);
            if (user == null)
            {
                _userDialogs.Alert(Constants.DidNotComplite);
                return;
            }

            var userDatabaseModel = new UserDatabaseModel
            {
                Name = user.First_name,
                Surname = user.Last_name,
                Email = user.Email,
                UserId = user.Id,
                PhotoURL = user.UserPicture.PictureData.Url,
                TypeUserLogin = LoginType.Facebook
            };

            string userPhoto = await _facebookService.GetImageFromUrlToBase64(userDatabaseModel.PhotoURL);

            userDatabaseModel.Photo = userPhoto;

            int userIdInDB = _sqlLiteRepository.SaveItem(userDatabaseModel);
            string idInDB = userIdInDB.ToString();

            _registrationService.UserRegistration(user.First_name, user.Email, idInDB);

            await _navigationService.Navigate<MainViewModel>();
        }

        public async Task OnGoogleAuthenticationCompleted(GoogleModel user)
        {
            if (user == null)
            {
                _userDialogs.Alert(Constants.DidNotComplite);
                return;
            }

            var userDatabaseModel = new UserDatabaseModel
            {
                Name = user.First_name,
                Surname = user.Last_name,
                Email = user.Email,
                UserId = user.Id,
                PhotoURL = user.Picture,
                TypeUserLogin = LoginType.Google
            };

            string userPhoto = await _facebookService.GetImageFromUrlToBase64(userDatabaseModel.PhotoURL);
            userDatabaseModel.Photo = userPhoto;

            int userIdInDB = _sqlLiteRepository.SaveItem(userDatabaseModel);
            string idInDB = userIdInDB.ToString();

            _registrationService.UserRegistration(user.First_name, user.Email, idInDB);

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

        public async Task SaveUserGoogleiOS(GoogleModeliOS user)
        {
            if (user == null)
            {
                _userDialogs.Alert(Constants.DidNotComplite);
                return;
            }

            var userDatabaseModel = new UserDatabaseModel
            {
                Name = user.UserName.GivenName,
                Surname = user.UserName.FamilyName,
                Email = user.Emails[0].Value,
                PhotoURL = user.UserImage.Url,
                TypeUserLogin = LoginType.Google
            };

            string userPhoto = await _facebookService.GetImageFromUrlToBase64(userDatabaseModel.PhotoURL);
            userDatabaseModel.Photo = userPhoto;
            int userIdInDB = _sqlLiteRepository.SaveItem(userDatabaseModel);
            string idInDB = userIdInDB.ToString();
            _registrationService.UserRegistration(userDatabaseModel.Name, userDatabaseModel.Email, idInDB);

            await _navigationService.Navigate<MainViewModel>();
        }
        #endregion Methods      
    }
}
