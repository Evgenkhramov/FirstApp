using FirstApp.Core.Interfaces;
using FirstApp.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstApp.Core.ViewModels
{
    public class MainFragmentViewModel : BaseViewModel
    {
        
        private readonly ISQLiteRepository _sQLiteRepository;

        public MainFragmentViewModel( ISQLiteRepository sQLiteRepository)
        {
            _sQLiteRepository = sQLiteRepository; 
            GetName();
            GetDateFromDb();
            HaveGone = false;
        }

        public MvxCommand LogOut
        {
            get
            {
                return new MvxCommand(async () =>
                {
                    CrossSecureStorage.Current.SetValue(Constants.SequreKeyForLoged, Constants.LogOut);

                    await NavigationService.Navigate<LoginFragmentViewModel>();
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
            Welcome = $"Welcome, {name}";
        }

        public void GetDateFromDb()
        {
            var userDate = new UserDatabaseModel();
            userDate = _sQLiteRepository.GetItem(1);
            DataFromBase = $"{userDate.Name} {userDate.Password}";
        }
    }
}
