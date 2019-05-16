using FirstApp.Core.Interfaces;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace FirstApp.Core.ViewModels
{
    public class StartViewModel : BaseViewModel
    {
        private readonly IAuthorizationService _authorizationService;
        public StartViewModel(IAuthorizationService authorizationService, IMvxNavigationService navigationService) : base(navigationService)
        {
            _authorizationService = authorizationService;
            ShowLoginFragmentCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<LoginViewModel>());            
        }
        public IMvxAsyncCommand ShowLoginFragmentCommand { get; private set; }
    }
}
