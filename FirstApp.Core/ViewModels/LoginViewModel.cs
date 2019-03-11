using MvvmCross.ViewModels;
using FirstApp.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.Commands;
using System.Windows.Input;
using MvvmCross;
using MvvmCross.Navigation;
using FirstApp.Core.Interfaces;
using Android.Widget;
using Android.Content;
using Android.App;
using Acr.UserDialogs;

namespace FirstApp.Core.ViewModels
{
    public class LoginViewModel : MvxViewModel
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IMvxNavigationService _navigationService;
        public LoginViewModel(IMvxNavigationService navigationService, IAuthorizationService authorizationService)
        {
            _navigationService = navigationService;
            _authorizationService = authorizationService;
            HaveGone = false;
            
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
        private bool _haveGone;
        public bool HaveGone
        {
            get => _haveGone;
            set
            {
                _haveGone = value;
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
                    var navService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();
                    await navService.Navigate<RegistrationViewModel>();
                });
            }
        }
    }
}
