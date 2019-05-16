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
        private readonly Regex PasswordRegExp = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,20}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private readonly Regex NameRegExp = new Regex(@"^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private readonly IRegistrationService _registrationService;
        private readonly IDBUserService _dBUserService;

        public RegistrationViewModel(IRegistrationService registrationService, IDBUserService dBUserService, IMvxNavigationService navigationService) : base(navigationService)
        {
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
                    //await _navigationService.Navigate<LoginViewModel>();
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
                    if (name && password && passwordConfirm)
                    {
                        var userDatabaseModel = new UserDatabaseModel
                        {
                            Name = RegistrationUserName,
                            Password = RegistrationUserPassword,
                            HowDoLogin = Enums.LoginMethod.App
                        };

                        if (!_dBUserService.IsLoginInDB(RegistrationUserName))
                        {
                            _dBUserService.SaveItem(userDatabaseModel);
                            string userId = userDatabaseModel.Id.ToString();
                            _registrationService.UserRegistration(RegistrationUserName, RegistrationUserPassword, userId);

                            await _navigationService.Close(this);
                            await _navigationService.Navigate<MainViewModel>();
                        }
                        else
                        {
                            Mvx.IoCProvider.Resolve<IUserDialogs>().Alert("This name is already in the database, enter other name");
                            //_userDialogService.ShowAlertForUser("Error", "This name is already in the database, enter other name ", "Ok");
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
                    Mvx.IoCProvider.Resolve<IUserDialogs>().Alert("Enter correct name");
                    return false;
                }
                return true;
            }
            else
            {
                Mvx.IoCProvider.Resolve<IUserDialogs>().Alert("Please, enter name");
                return false;
            }
        }
        private bool PasswordValidator()
        {
            if (!string.IsNullOrEmpty(RegistrationUserPassword))
            {
                if (!PasswordRegExp.IsMatch(RegistrationUserPassword))
                {
                    Mvx.IoCProvider.Resolve<IUserDialogs>().Alert("Password must have minimum eight characters, at least one letter and one numbe!");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                Mvx.IoCProvider.Resolve<IUserDialogs>().Alert("Enter Password!");
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
                    Mvx.IoCProvider.Resolve<IUserDialogs>().Alert("Password and password confirm must be the same!");

                    return false;
                }
            }
            else
            {
                Mvx.IoCProvider.Resolve<IUserDialogs>().Alert("Enter PasswordConfirm!");

                return false;
            }
        }
    }

}

