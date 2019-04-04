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
    public class LoginFragmentViewModel : BaseViewModel, IFacebookAuthenticationDelegate
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IRegistrationService _registrationService;
        private readonly ISQLiteRepository _sqlLiteRepository;
        private readonly IFacebookService _facebookService;


        public LoginFragmentViewModel(IAuthorizationService authorizationService, ISQLiteRepository sQLiteRepository, IRegistrationService registrationService, IFacebookService facebookService)
        {
            ShowMainViewModelCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<MainViewModel>());
            ShowLoginFragmentViewModelCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<LoginFragmentViewModel>());
            _authorizationService = authorizationService;
            _sqlLiteRepository = sQLiteRepository;
            _registrationService = registrationService;
            _facebookService = facebookService;
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

            NavigationService.Navigate<MainViewModel>();
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

            NavigationService.Navigate<MainViewModel>();
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
                        await NavigationService.Navigate<MainViewModel>();
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
                    var navService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();
                    await navService.Navigate<RegistrationFragmentViewModel>();
                });
            }
        }
    }
}
