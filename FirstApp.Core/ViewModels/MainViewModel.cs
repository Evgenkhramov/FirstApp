using MvvmCross.ViewModels;
using System;
using FirstApp.Core.Services;
using System.Collections.Generic;
using System.Text;
using MvvmCross.Commands;
using Plugin.SecureStorage;
using MvvmCross.Navigation;
using MvvmCross;

namespace FirstApp.Core.ViewModels
{
   public class MainViewModel : MvxViewModel
    {
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
                    CrossSecureStorage.Current.SetValue(Constants.SequreKeyForLoged, "");
                    var navService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();
                    await navService.Navigate<LoginViewModel>();
                    //sfcsfdewdweddwdw
                });
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
                GetName();
            }
        }
        public string GetName()
        {
            string name = CrossSecureStorage.Current.GetValue(Constants.SequreKeyForUserName);
            return $"Welcome, {name}";
        }
       
        //private void CheckData()
        //{
        //    bool isAutorisated = _authorizationService.IsLoggedIn(UserName);
        //}
    }
}
