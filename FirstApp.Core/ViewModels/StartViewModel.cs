using FirstApp.Core.Interfaces;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstApp.Core.ViewModels
{
    public class StartViewModel : BaseViewModel
    {
        private readonly IAuthorizationService _authorizationService;
        public StartViewModel(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
            ShowLoginFragmentCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<LoginFragmentViewModel>());
        }
        public IMvxAsyncCommand ShowLoginFragmentCommand { get; private set; }
    }
}
