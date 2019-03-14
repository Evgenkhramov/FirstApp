using FirstApp.Core.Interfaces;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstApp.Core.ViewModels
{
    public class StartViewModel : MvxViewModel
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IMvxNavigationService _navigationService;
        public StartViewModel(IMvxNavigationService navigationService, IAuthorizationService authorizationService)
        {
            _navigationService = navigationService;
            _authorizationService = authorizationService;
            ShowLoginFragmentCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<LoginFragmentViewModel>());
        }
        public IMvxAsyncCommand ShowLoginFragmentCommand { get; private set; }
    }
}
