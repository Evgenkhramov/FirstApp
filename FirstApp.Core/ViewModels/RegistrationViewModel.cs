using Acr.UserDialogs;
using FirstApp.Core.Interfaces;
using FirstApp.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System.Text.RegularExpressions;

namespace FirstApp.Core.ViewModels
{
    public class RegistrationViewModel : BaseViewModel
    {
        #region Variables

        private readonly Regex _passwordRegExp = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,20}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private readonly Regex _nameRegExp = new Regex(@"^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private readonly IRegistrationService _registrationService;
        private readonly IDBUserService _dBUserService;

        #endregion Variables

        #region Constructors

        public RegistrationViewModel(IRegistrationService registrationService, IDBUserService dBUserService,
            IMvxNavigationService navigationService, IUserDialogs userDialogs) : base(navigationService, userDialogs)
        {
            _registrationService = registrationService;
            _dBUserService = dBUserService;
            HaveGone = false;
            SaveButton = false;
        }

        #endregion Constructors

        #region Properties

        public bool SaveButton { get; set; }

        public bool HaveGone { get; set; }

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

        #endregion Properties

        #region Commands  

        public IMvxAsyncCommand NavigateCommand { get; private set; }

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
                        _userDialogs.Alert(Constants.ThisNameIsUsed);
                    }

                    if (name && password && passwordConfirm && !_dBUserService.IsLoginInDB(RegistrationUserName))
                    {
                        var userDatabaseModel = new UserDatabaseModel
                        {
                            Name = RegistrationUserName,
                            Password = RegistrationUserPassword,
                            TypeUserLogin = LoginType.App
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

        #endregion Commands

        #region Methods

        private bool NameValidator()
        {
            if (!string.IsNullOrEmpty(RegistrationUserName) && _nameRegExp.IsMatch(RegistrationUserName))
            {
                return true;
            }

            if (!string.IsNullOrEmpty(RegistrationUserName) && !_nameRegExp.IsMatch(RegistrationUserName))
            {
                _userDialogs.Alert(Constants.UseCorrectName);

                return false;
            }

            _userDialogs.Alert(Constants.EnterName);

            return false;
        }

        private bool PasswordValidator()
        {
            
            if (!string.IsNullOrEmpty(RegistrationUserPassword) && _passwordRegExp.IsMatch(RegistrationUserPassword))
            {
                return true;
            }

            if (!string.IsNullOrEmpty(RegistrationUserPassword) && !_passwordRegExp.IsMatch(RegistrationUserPassword))
            {
                _userDialogs.Alert(Constants.CorrectPassword);

                return false;
            }

            _userDialogs.Alert(Constants.EnterPassword);

            return false;
        }

        private bool PasswordConfirmValidator()
        {
            if (!string.IsNullOrEmpty(RegistrationUserPasswordConfirm) && _passwordRegExp.IsMatch(RegistrationUserPasswordConfirm) && (RegistrationUserPassword.Equals(RegistrationUserPasswordConfirm)))
            {
                return true;
            }

            if (!string.IsNullOrEmpty(RegistrationUserPasswordConfirm) && _passwordRegExp.IsMatch(RegistrationUserPasswordConfirm) && !(RegistrationUserPassword.Equals(RegistrationUserPasswordConfirm)))
            {
                _userDialogs.Alert(Constants.PasswordConfirm);
                return false;
            }

            if (!string.IsNullOrEmpty(RegistrationUserPasswordConfirm) && !_passwordRegExp.IsMatch(RegistrationUserPasswordConfirm))
            {
                _userDialogs.Alert(Constants.CorrectPassword);

                return false;
            }

            _userDialogs.Alert(Constants.EnterPasswordConfirm);

            return false;

        }

        #endregion Methods
    }
}


