using Acr.UserDialogs;
using FirstApp.Core.Interfaces;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace FirstApp.Core.ViewModels
{
    public class StartViewModel : BaseViewModel
    {

        #region Constructors

        public StartViewModel(IAuthorizationService authorizationService, IMvxNavigationService navigationService, IUserDialogs userDialogs) : base(navigationService, userDialogs)
        {

            ShowLoginFragmentCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<LoginViewModel>());
        }

        #endregion Constructors

        #region Commands 

        public IMvxAsyncCommand ShowLoginFragmentCommand { get; private set; }

        #endregion Commands
    }
}
