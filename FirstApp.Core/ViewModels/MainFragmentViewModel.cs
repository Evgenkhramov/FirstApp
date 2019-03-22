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
        private int userId;
        private readonly ISQLiteRepository _sQLiteRepository;


        public MainFragmentViewModel( ISQLiteRepository sQLiteRepository)
        {
            _sQLiteRepository = sQLiteRepository; 
            
            string id = (CrossSecureStorage.Current.GetValue(Constants.SequreKeyForUserIdInDB));
            userId = Int32.Parse(id);
            GetDataFromDB(userId);
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

        public void GetDataFromDB(int userId)
        {
            var userDate = new UserDatabaseModel();
            userDate = _sQLiteRepository.GetItem(userId);
            Welcome = $"Welcome, {userDate.Name} {userDate.Surname}";
        }
    }
}
