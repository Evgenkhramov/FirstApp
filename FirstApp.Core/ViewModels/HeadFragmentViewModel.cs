using Acr.UserDialogs;
using FirstApp.Core.Interfaces;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstApp.Core.ViewModels
{
    public class HeadFragmentViewModel : MvxViewModel
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IMvxNavigationService _navigationService;
        public HeadFragmentViewModel(IMvxNavigationService navigationService, IAuthorizationService authorizationService)
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
                        await _navigationService.Close(this);

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
