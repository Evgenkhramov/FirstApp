using FirstApp.Core.Services;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using FirstApp.Core.Interfaces;

namespace FirstApp.Core.ViewModels
{
    public class RegistrationViewModel : MvxViewModel
    {
        private readonly IRegistrationService _registrationService;
        private readonly IMvxNavigationService _navigationService;

        public RegistrationViewModel(IMvxNavigationService navigationService, IRegistrationService registrationService)
        {
            _navigationService = navigationService;
            _registrationService = registrationService;
        }

        public IMvxAsyncCommand NavigateCommand { get; private set; }
        
        private string _registrationUserName;
        public string RegistrationUserName
        {
            get => _registrationUserName;
            set
            {
                _registrationUserName = value;
            }
        }

        private string _registrationUserPassword;
        public string RegistrationUserPassword
        {
            get => _registrationUserPassword;
            set
            {
                _registrationUserPassword = value;
            }
        }

        public MvxAsyncCommand UserRegistration
        {
            get
            {
                return new MvxAsyncCommand(async () =>
                {
                    _registrationService.UserRegistration(RegistrationUserName, RegistrationUserPassword);

                    await _navigationService.Navigate<MainViewModel>();
                });
            }
        }

    }
}
