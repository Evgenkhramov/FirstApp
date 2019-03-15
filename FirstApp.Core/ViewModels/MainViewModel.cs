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

namespace FirstApp.Core.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly ISQLiteRepository _sQLiteRepository;

        public MainViewModel(IMvxNavigationService navigationService, ISQLiteRepository sQLiteRepository)
        {

            _sQLiteRepository = sQLiteRepository;
            _navigationService = navigationService;
           ShowMainFragmentCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<MainFragmentViewModel>());
        }
        public IMvxAsyncCommand ShowMainFragmentCommand { get; private set; }
    }   
}
