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
using Acr.UserDialogs;
using System.Text.RegularExpressions;

namespace FirstApp.Core.ViewModels
{
    public class RegistrationViewModel : MvxViewModel
    {
        private readonly Regex PasswordRegExp = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,20}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private readonly Regex NameRegExp = new Regex(@"^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private readonly IRegistrationService _registrationService;
        private readonly IMvxNavigationService _navigationService;
        private IUserDialogs _userDialogs ;

        public RegistrationViewModel(IMvxNavigationService navigationService, IRegistrationService registrationService, IUserDialogs userDialogs)
        {
            _userDialogs = userDialogs;
            _navigationService = navigationService;
            _registrationService = registrationService;
            HaveGone = true;
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

       

        private bool _haveGone;
        public bool HaveGone
        {
            get => _haveGone;
            set
            {
                _haveGone = value;
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
                    bool name = false;
                    bool password = false;
                    bool passwordConfirm = false;

                    name =  NameValidator();
                    password = PasswordValidator();
                    passwordConfirm = PasswordConfirmValidator();              

                        //if ((!String.IsNullOrEmpty(RegistrationUserName) && !String.IsNullOrEmpty(RegistrationUserPassword) && !String.IsNullOrEmpty(RegistrationUserPasswordConfirm)) && RegistrationUserPassword.Equals(RegistrationUserPasswordConfirm))
                    if(name && password && passwordConfirm)
                    {
                        _registrationService.UserRegistration(RegistrationUserName, RegistrationUserPassword);

                        await _navigationService.Navigate<MainViewModel>();
                    }
                    //else
                    //{
                    //    Mvx.IoCProvider.Resolve<IUserDialogs>().Alert("Please, enter name, password and password confirm!");

                    //    //Context context = Application.Context;
                    //    //string text = "Please, enter name, password and password confirm!";
                    //    //ToastLength duration = ToastLength.Short;

                    //    //var toast = Toast.MakeText(context, text, duration);
                    //    //toast.Show();
                    //}
                });
            }

        }

        private bool NameValidator()
        {
            if (!string.IsNullOrEmpty(RegistrationUserName))
            {
                if (!NameRegExp.IsMatch(RegistrationUserName))
                {
                    _userDialogs.Alert("Enter correct name!");
                    return false;
                }
                return true;
            }
            else
            {
                _userDialogs.Alert("Enter name!");
                return false;
            }
        }
        private bool PasswordValidator()
        {
            if (!string.IsNullOrEmpty(RegistrationUserPassword))
            {
                if (!PasswordRegExp.IsMatch(RegistrationUserPassword))
                {
                    _userDialogs.Alert("Password must have minimum eight characters, at least one letter and one numbe!");
                    return false;
                }
                else
                {
                   return true;
                }
            }
            else
            {
                _userDialogs.Alert("Enter Password!");
                return false;
            }
        }
        private bool PasswordConfirmValidator()
        {
            if (!string.IsNullOrEmpty(RegistrationUserPasswordConfirm))
            {
                if (PasswordRegExp.IsMatch(RegistrationUserPasswordConfirm) && (RegistrationUserPassword.Equals(RegistrationUserPasswordConfirm)))
                {
                    return true;
                }
                else
                {
                    _userDialogs.Alert("Password and password confirm must be the same!");
                    return false;
                }
            }
            else
            {
                _userDialogs.Alert("Enter PasswordConfirm!");
                return false;
            }
        }
    }

}

