using MvvmCross.ViewModels;
using System;
using FirstApp.Core.Services;
using System.Collections.Generic;
using System.Text;
using MvvmCross.Commands;
using Plugin.SecureStorage;
using MvvmCross.Navigation;
using MvvmCross;
using FirstApp.Core.Interfaces;
using FirstApp.Core.Models;
using System.Threading.Tasks;

namespace FirstApp.Core.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel(IMvxNavigationService navigationService) : base(navigationService)
        {
            ShowMainFragmentCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<TaskListViewModel>());
            ShowMenuViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<MenuViewModel>());
        }
        public IMvxAsyncCommand ShowMainFragmentCommand { get; private set; }
        public IMvxAsyncCommand ShowMenuViewModelCommand { get; private set; }

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

