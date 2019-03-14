using FirstApp.Core.Interfaces;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstApp.Core.ViewModels
{
    public class HeadViewModel : MvxViewModel
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IMvxNavigationService _navigationService;
        public HeadViewModel(IMvxNavigationService navigationService, IAuthorizationService authorizationService)
        {
            _navigationService = navigationService;
            _authorizationService = authorizationService;
            // HaveGone = false;

        }
    }
}
