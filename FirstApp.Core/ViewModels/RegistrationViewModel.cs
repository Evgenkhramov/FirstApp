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
        private readonly Regex _emailRegExp = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private readonly IRegistrationService _registrationService;
        private readonly IUserService _userService;

        #endregion Variables

        #region Constructors

        public RegistrationViewModel(IRegistrationService registrationService, IUserService userService,
            IMvxNavigationService navigationService, IUserDialogs userDialogs) : base(navigationService, userDialogs)
        {
            _registrationService = registrationService;
            _userService = userService;

            HaveGone = false;
            SaveButton = true;
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

        private string _registrationEmail;
        public string RegistrationEmail
        {
            get => _registrationEmail;
            set
            {
                _registrationEmail = value;
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
                    bool isNameValid = false;
                    bool isEmailValid = false;
                    bool isPasswordValid = false;
                    bool isPasswordConfirmValid = false;

                    isEmailValid = CheckEmailIsValid();

                    isNameValid = CheckNameIsValid();

                    if (isNameValid && isEmailValid)
                    {
                        isPasswordValid = CheckPasswordIsValid();
                    }

                    if (isPasswordValid)
                    {
                        isPasswordConfirmValid = CheckPasswordConfirmIsValid();
                    }

                    if (isNameValid && isEmailValid && isPasswordValid && isPasswordConfirmValid && _userService.CheckEmailInDB(RegistrationEmail))
                    {
                        _userDialogs.Alert(Constants.ThisEmailIsUsed);
                    }

                    if (isNameValid && isEmailValid && isPasswordValid && isPasswordConfirmValid && !_userService.CheckEmailInDB(RegistrationEmail))
                    {
                        int userId = _registrationService.SaveUserFromApp(RegistrationUserName, RegistrationUserPassword, RegistrationEmail, LoginType.App);

                        _registrationService.UserRegistration(userId.ToString());

                        await _navigationService.Close(this);
                        await _navigationService.Navigate<MainViewModel>();
                    }
                });
            }
        }

        #endregion Commands

        #region Methods

        private bool CheckNameIsValid()
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

        private bool CheckPasswordIsValid()
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

        private bool CheckPasswordConfirmIsValid()
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

        private bool CheckEmailIsValid()
        {
            if (!string.IsNullOrEmpty(RegistrationEmail) && _emailRegExp.IsMatch(RegistrationEmail))
            {
                return true;
            }

            if (!string.IsNullOrEmpty(RegistrationEmail) && !_emailRegExp.IsMatch(RegistrationEmail))
            {
                _userDialogs.Alert(Constants.EnterCorrectEmail);

                return false;
            }

            _userDialogs.Alert(Constants.EnterEmail);

            return false;
        }

        #endregion Methods
    }
}


