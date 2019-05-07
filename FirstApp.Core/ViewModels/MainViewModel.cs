using MvvmCross.Commands;
using MvvmCross.Navigation;
using System.Threading.Tasks;

namespace FirstApp.Core.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel(IMvxNavigationService navigationService) : base(navigationService)
        {
            ShowMainFragmentCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<TaskListViewModel>());
            ShowMenuViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<MenuViewModel>());
            ShowUserProfileViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<UserDataViewModel>());
        }
        public IMvxAsyncCommand ShowMainFragmentCommand { get; private set; }
        public IMvxAsyncCommand ShowMenuViewModelCommand { get; private set; }
        public IMvxAsyncCommand ShowUserProfileViewModelCommand { get; private set; }

        public async Task ShowMain()
        {
            await _navigationService.Navigate<MenuViewModel>();
            await _navigationService.Navigate<TaskListViewModel>();
        }
        public async Task ShowMainIOS()
        {
            await _navigationService.Navigate<TaskListViewModel>();
        }
    }
}

