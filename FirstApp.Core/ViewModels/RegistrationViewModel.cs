using MvvmCross.Commands;
using MvvmCross.Navigation;
using FirstApp.Core.Interfaces;
using FirstApp.Core.Models;
using Acr.UserDialogs;
using System.Text.RegularExpressions;
using MvvmCross;

namespace FirstApp.Core.ViewModels
{
    public class RegistrationViewModel : BaseViewModel
    {
        private readonly Regex _passwordRegExp;
        private readonly Regex _nameRegExp;
        private readonly IRegistrationService _registrationService;
        private readonly IDBUserService _dBUserService;

        public RegistrationViewModel(IRegistrationService registrationService, IDBUserService dBUserService, IMvxNavigationService navigationService) : base(navigationService)
        {
            _passwordRegExp = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,20}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            _nameRegExp = new Regex(@"^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            _registrationService = registrationService;
            _dBUserService = dBUserService;
            HaveGone = false;
            SaveButton = false;
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

        private bool _saveButton;
        public bool SaveButton
        {
            get => _saveButton;
            set
            {
                _saveButton = value;
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

        public MvxAsyncCommand BackViewCommand
        {
            get
            {
                return new MvxAsyncCommand(async () =>
                {
                    await _navigationService.Close(this);
                });
            }
        }

        public MvxAsyncCommand UserRegistrationCommand
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

                    if (name && password && passwordConfirm && _dBUserService.IsLoginInDB(RegistrationUserName))
                    {
                        Mvx.IoCProvider.Resolve<IUserDialogs>().Alert(Constants.ThisNameIsUsed);
                    }

                    if (name && password && passwordConfirm && !_dBUserService.IsLoginInDB(RegistrationUserName))
                    {
                        var userDatabaseModel = new UserDatabaseModel
                        {
                            Name = RegistrationUserName,
                            Password = RegistrationUserPassword,
                            HowDoLogin = Enums.LoginMethod.App
                        };
                        _dBUserService.SaveItem(userDatabaseModel);
                        string userId = userDatabaseModel.Id.ToString();
                        _registrationService.UserRegistration(RegistrationUserName, RegistrationUserPassword, userId);

                        await _navigationService.Close(this);
                        await _navigationService.Navigate<MainViewModel>();
                    }
                });
            }
        }

        private bool NameValidator()
        {
            if (!string.IsNullOrEmpty(RegistrationUserName))
            {
                if (!_nameRegExp.IsMatch(RegistrationUserName))
                {
                    Mvx.IoCProvider.Resolve<IUserDialogs>().Alert(Constants.UseCorrectName);

                    return false;
                }

                return true;
            }

            Mvx.IoCProvider.Resolve<IUserDialogs>().Alert(Constants.EnterName);

            return false;
        }

        private bool PasswordValidator()
        {
            if (!string.IsNullOrEmpty(RegistrationUserPassword))
            {
                if (!_passwordRegExp.IsMatch(RegistrationUserPassword))
                {
                    Mvx.IoCProvider.Resolve<IUserDialogs>().Alert(Constants.CorrectPassword);
                    return false;
                }

                return true;
            }
            else
            {
                Mvx.IoCProvider.Resolve<IUserDialogs>().Alert(Constants.EnterPassword);

                return false;
            }
        }

        private bool PasswordConfirmValidator()
        {
            if (!string.IsNullOrEmpty(RegistrationUserPasswordConfirm))
            {
                if (_passwordRegExp.IsMatch(RegistrationUserPasswordConfirm) && (RegistrationUserPassword.Equals(RegistrationUserPasswordConfirm)))
                {
                    return true;
                }
                Mvx.IoCProvider.Resolve<IUserDialogs>().Alert(Constants.PasswordConfirm);

                return false;
            }

            Mvx.IoCProvider.Resolve<IUserDialogs>().Alert(Constants.EnterPasswordConfirm);

            return false;
        }
    }
}

