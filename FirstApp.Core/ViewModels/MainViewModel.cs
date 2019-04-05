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
        public MainViewModel()
        {

            ShowMainFragmentCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<MainFragmentViewModel>());
            ShowMenuViewModelCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<MenuFragmentViewModel>());
        }
        public IMvxAsyncCommand ShowMainFragmentCommand { get; private set; }
        public IMvxAsyncCommand ShowMenuViewModelCommand { get; private set; }

        public async System.Threading.Tasks.Task ShowMain()
        {
            await NavigationService.Navigate<MenuFragmentViewModel>();
            await NavigationService.Navigate<MainFragmentViewModel>();
        }
        public class TaskList
        {
            public void AddTask()
            {

            }

            public void DeleteTask(int id)
            {

            }

            public void EditTask(int id)
            {

            }

        }

    }
}

