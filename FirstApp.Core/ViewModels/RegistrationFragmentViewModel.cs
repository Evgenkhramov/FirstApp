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
using FirstApp.Core.Models;
using Android.Widget;
using Android.App;
using Acr.UserDialogs;
using System.Text.RegularExpressions;
using System.Linq;

namespace FirstApp.Core.ViewModels
{
    public class RegistrationFragmentViewModel : BaseViewModel
    {
        private readonly Regex PasswordRegExp = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,20}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private readonly Regex NameRegExp = new Regex(@"^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private readonly IRegistrationService _registrationService;
        private readonly IUserDialogService _userDialogService;
        private readonly ISQLiteRepository _sQLiteRepository;

        public RegistrationFragmentViewModel(IRegistrationService registrationService,
            IUserDialogs userDialogs, IUserDialogService userDialogService, ISQLiteRepository sQLiteRepository)
        {
            _userDialogService = userDialogService;
            _registrationService = registrationService;
            _sQLiteRepository = sQLiteRepository;
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

        private bool _haveGone;
        public bool HaveGone
        {
            get => _haveGone;
            set
            {
                _haveGone = value;
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
                    await NavigationService.Close(this);
                    await NavigationService.Navigate<LoginFragmentViewModel>();
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

                    name = NameValidator();
                    if (name)
                    {
                        password = PasswordValidator();
                    }
                    if (password)
                    {
                        passwordConfirm = PasswordConfirmValidator();
                    }
                    if (name && password && passwordConfirm)
                    {
                        var userDatabaseModel = new UserDatabaseModel
                        {
                            Name = RegistrationUserName,
                            Password = RegistrationUserPassword
                        };

                        if (!_sQLiteRepository.IsLoginInDB(RegistrationUserName))
                        {
                            _sQLiteRepository.SaveItem(userDatabaseModel);
                            string userId = userDatabaseModel.Id.ToString();
                            _registrationService.UserRegistration(RegistrationUserName, RegistrationUserPassword, userId);

                            await NavigationService.Close(this);
                            await NavigationService.Navigate<MainViewModel>();
                        }
                        else
                        {
                            _userDialogService.ShowAlertForUser("Error", "This name is already in the database, enter other name ", "Ok");
                        }
                                         
                    }
                });
            }
        }

        private bool NameValidator()
        {
            if (!string.IsNullOrEmpty(RegistrationUserName))
            {
                if (!NameRegExp.IsMatch(RegistrationUserName))
                {
                    _userDialogService.ShowAlertForUser("Error", "Enter correct name", "Ok");
                    return false;
                }
                return true;
            }
            else
            {
                _userDialogService.ShowAlertForUser("Error", "Please, enter name", "Ok");
                return false;
            }
        }
        private bool PasswordValidator()
        {
            if (!string.IsNullOrEmpty(RegistrationUserPassword))
            {
                if (!PasswordRegExp.IsMatch(RegistrationUserPassword))
                {
                    _userDialogService.ShowAlertForUser("Error", "Password must have minimum eight characters, at least one letter and one numbe!", "Ok");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                _userDialogService.ShowAlertForUser("Error", "Enter Password!", "Ok");
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
                    _userDialogService.ShowAlertForUser("Error", "Password and password confirm must be the same!", "Ok");
                    return false;
                }
            }
            else
            {
                _userDialogService.ShowAlertForUser("Error", "Enter PasswordConfirm!", "Ok");
                return false;
            }
        }
    }

}

