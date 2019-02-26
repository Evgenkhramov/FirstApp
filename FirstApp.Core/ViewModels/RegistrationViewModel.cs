using FirstApp.Core.Services;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using FirstApp.Core.Interfaces;
using Android.Content;

using Android.Widget;
using Android.App;

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
        private string _registrationUserPasswordConfirm;
        public string RegistrationUserPasswordConfirm
        {
            get => _registrationUserPasswordConfirm;
            set
            {
                _registrationUserPasswordConfirm = value;
            }
        }

        public MvxAsyncCommand RegistrationBack
        {
            get
            {
                return new MvxAsyncCommand(async () =>
                {

                    await _navigationService.Navigate<LoginViewModel>();

                });
            }
        }

        public MvxAsyncCommand UserRegistration
        {
            get
            {
                return new MvxAsyncCommand(async () =>
                {
                    if ((!String.IsNullOrEmpty(RegistrationUserName) && !String.IsNullOrEmpty(RegistrationUserPassword) && !String.IsNullOrEmpty(RegistrationUserPasswordConfirm)) && RegistrationUserPassword.Equals(RegistrationUserPasswordConfirm))
                    {
                        _registrationService.UserRegistration(RegistrationUserName, RegistrationUserPassword);

                        await _navigationService.Navigate<MainViewModel>();
                    }
                    else
                    {
                        Context context = Application.Context;
                        string text = "Please, enter name, password and password confirm!";
                        ToastLength duration = ToastLength.Short;

                        var toast = Toast.MakeText(context, text, duration);
                        toast.Show();
                    }
                });
            }

        }
    }

}

