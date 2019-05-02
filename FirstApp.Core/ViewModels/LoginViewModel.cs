using Acr.UserDialogs;
using FirstApp.Core.Authentication;
using FirstApp.Core.Interfaces;
using FirstApp.Core.Models;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Plugin.SecureStorage;
using System;
using System.Threading.Tasks;

namespace FirstApp.Core.ViewModels
{
    public class LoginViewModel : BaseViewModel, IFacebookAuthenticationDelegate
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IRegistrationService _registrationService;
        private readonly IDBUserService _sqlLiteRepository;
        private readonly IFacebookService _facebookService;

        public LoginViewModel(IAuthorizationService authorizationService, IDBUserService sQLiteRepository, IRegistrationService registrationService, IFacebookService facebookService,IMvxNavigationService navigationService) : base(navigationService)
        {
            ShowMainViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<MainViewModel>());
            ShowLoginFragmentViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<LoginViewModel>());
            _authorizationService = authorizationService;
            _sqlLiteRepository = sQLiteRepository;
            _registrationService = registrationService;
            _facebookService = facebookService;
            HaveGone = true;
            SaveButton = true;
        }

        private bool _haveGone;
        public bool HaveGone
        {
            get => _haveGone;
            set
            {
                _haveGone = value;
            }
        }

        private bool _saveButton;
        public bool SaveButton
        {
            get => _saveButton;
            set
            {
                _saveButton = value;
            }
        }

        public IMvxAsyncCommand ShowMainViewModelCommand { get; private set; }
        public IMvxAsyncCommand ShowLoginFragmentViewModelCommand { get; private set; }

        public async Task OnAuthenticationCompleted(FacebookOAuthToken token)
        {
            var user = await _facebookService.GetUserDataAsync(token.AccessToken);
            if (user == null)
            {
                return;
            }
            var userDatabaseModel = new UserDatabaseModel();
            userDatabaseModel.Name = user.First_name;
            userDatabaseModel.Surname = user.Last_name;
            userDatabaseModel.Email = user.Email;
            userDatabaseModel.UserId = user.Id;
            userDatabaseModel.PhotoURL = user.picture.data.url;
            userDatabaseModel.HowDoLogin = Enums.LoginMethod.Facebook;

            string userPhoto = await _facebookService.GetImageFromUrlToBase64(userDatabaseModel.PhotoURL);
            userDatabaseModel.Photo = userPhoto;

            int userIdInDB = _sqlLiteRepository.SaveItem(userDatabaseModel);
            string idInDB = userIdInDB.ToString();
            _registrationService.UserRegistration(user.First_name, user.Email, idInDB);

            _navigationService.Navigate<MainViewModel>();
        }

        public async Task OnGoogleAuthenticationCompleted(GoogleModel user)
        {          
            var userDatabaseModel = new UserDatabaseModel();
            userDatabaseModel.Name = user.First_name;
            userDatabaseModel.Surname = user.Last_name;
            userDatabaseModel.Email = user.Email;
            userDatabaseModel.UserId = user.Id;
            userDatabaseModel.PhotoURL = user.Picture;
            userDatabaseModel.HowDoLogin = Enums.LoginMethod.Google;

            string userPhoto = await _facebookService.GetImageFromUrlToBase64(userDatabaseModel.PhotoURL);
            userDatabaseModel.Photo = userPhoto;

            int userIdInDB = _sqlLiteRepository.SaveItem(userDatabaseModel);
            string idInDB = userIdInDB.ToString();
            _registrationService.UserRegistration(user.First_name, user.Email, idInDB);

            _navigationService.Navigate<MainViewModel>();
        }

        public async Task OnAuthenticationCanceled()
        {
            Mvx.IoCProvider.Resolve<IUserDialogs>().Alert("You didn't completed the authentication process");
        }

        public async Task OnAuthenticationFailed()
        {
            Mvx.IoCProvider.Resolve<IUserDialogs>().Alert("You didn't completed the authentication process");
        }
        
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
                    else
                    {
                        Mvx.IoCProvider.Resolve<IUserDialogs>().Alert("Invalid username or password!");
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
    }
}
