using Acr.UserDialogs;
using FirstApp.Core.Interfaces;
using FirstApp.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System.Threading.Tasks;

namespace FirstApp.Core.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region Variables

        private readonly ICurrentPlatformService _currentPlatformService;

        #endregion Variables

        #region Constructors

        public MainViewModel(IMvxNavigationService navigationService, IUserDialogs userDialogs, ICurrentPlatformService currentPlatformService) : base(navigationService, userDialogs)
        {
            _currentPlatformService = currentPlatformService;
            ShowMainFragmentCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<TaskListViewModel>());
            ShowMenuViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<MenuViewModel>());
            ShowUserProfileViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<UserDataViewModel>());
        }

        #endregion Constructors

        #region Commands  

        public IMvxAsyncCommand ShowMainFragmentCommand { get; private set; }
        public IMvxAsyncCommand ShowMenuViewModelCommand { get; private set; }
        public IMvxAsyncCommand ShowUserProfileViewModelCommand { get; private set; }

        #endregion Commands

        #region Methods

        public async Task ShowMain()
        {
            if (_currentPlatformService.GetCurrentPlatform() == CurrentPlatformType.Android)
            {
                await _navigationService.Navigate<MenuViewModel>();
            }
            await _navigationService.Navigate<TaskListViewModel>();
        }

        //public async Task ShowMainIOS()
        //{
        //    await _navigationService.Navigate<TaskListViewModel>();
        //}

        #endregion Methods
    }
}

