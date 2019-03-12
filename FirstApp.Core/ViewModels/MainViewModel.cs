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
        //public MainViewModel()
        //{
        //    GetName();
        //}

        public MainViewModel(IMvxNavigationService navigationService, ISQLiteRepository sQLiteRepository)
        {
          
            _sQLiteRepository = sQLiteRepository;
            _navigationService = navigationService;
            HaveGone = false;
            GetName();
            GetDateFromDb();
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

        private string _dataFromBase;
        public string DataFromBase
        {
            get => _dataFromBase;
            set
            {
                _dataFromBase = value;
                RaisePropertyChanged(() => DataFromBase);                
            }
        }
        public void GetName()
        {
            string name = CrossSecureStorage.Current.GetValue(Constants.SequreKeyForUserName);
            Welcome =$"Welcome, {name}";
        }

        public void GetDateFromDb()
        {
            var userDate = new UserDatabaseModel();
            userDate = _sQLiteRepository.GetItem(1);
            DataFromBase = $"{userDate.Name} {userDate.Password}";
        }
    }
}
