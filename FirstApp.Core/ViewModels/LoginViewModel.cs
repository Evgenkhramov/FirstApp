using MvvmCross.ViewModels;
using FirstApp.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.Commands;
using System.Windows.Input;
using MvvmCross;
using MvvmCross.Navigation;

namespace FirstApp.Core.ViewModels
{
    public class LoginViewModel : MvxViewModel
    {
        RegistrationService registrationService = new RegistrationService();
        AuthorizationService autorizationService = new AuthorizationService();
        private string _userName;
        public string UserName
        {
            get => _userName;
            set
            {
                _userName= value;
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
                return new MvxAsyncCommand(async() =>
                {
                    if (autorizationService.IsLoggedIn(UserName, UserPassword))
                    {
                        var navService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();
                        await navService.Navigate<MainViewModel>();
                    }

                });
            }
        }
        //public IMvxCommand AddNumbers
        //{
        //    get
        //    {
        //        return _addNumbers = _addNumbers ?? new MvxCommand<Tuple<int, int>>(OnAddNumbers);
        //    }
        //}

        public MvxCommand UserRegistration
        {
            get
            {
                return new MvxCommand(() =>
                {
                    registrationService.UserRegistration(UserName, UserPassword);
                });
            }
        }
      
        //public ICommand UserRegistration
        //{
        //    get
        //    {
        //        return new MvxCommand(() => ShowViewModel<SecondViewModel>(), () => true);
        //    }
        //}

    }
}
