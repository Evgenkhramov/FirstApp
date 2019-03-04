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

namespace FirstApp.Core.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        //public MainViewModel()
        //{
        //    GetName();
        //}

        public MainViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            HaveGone = false;
        }

        readonly IAuthorizationService _authorizationService;

        public MainViewModel(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }
        public MvxCommand LogOut
        {
            get
            {
                return new MvxCommand(async () =>
                {
                    CrossSecureStorage.Current.SetValue(Constants.SequreKeyForLoged, Constants.LogOut);

                    await _navigationService.Navigate<LoginViewModel>();
                });
            }
        }

        private bool _haveGone;
        public bool HaveGone
        {
            get => _haveGone;
            set
            {
                _haveGone = value;
            }
        }

        private string _welcome;
        public string Welcome
        {
            get => _welcome;
            set
            {
                _welcome = value;
                RaisePropertyChanged(() => Welcome);                
            }
        }
        public void GetName()
        {
            string name = CrossSecureStorage.Current.GetValue(Constants.SequreKeyForUserName);
            Welcome =$"Welcome, {name}";
        } 
    }
}
