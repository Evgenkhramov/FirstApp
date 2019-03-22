using FirstApp.Core.Interfaces;
using FirstApp.Core.Models;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace FirstApp.Core.ViewModels
{
    public class MenuFragmentViewModel : BaseViewModel
    {
        private readonly ISQLiteRepository _sQLiteRepository;
        private int userId;
        private UserDatabaseModel userData;
        public MenuFragmentViewModel(ISQLiteRepository sQLiteRepository)
        {
            _sQLiteRepository = sQLiteRepository;
            //ShowLoginCommand = LogOut;
            string id = (CrossSecureStorage.Current.GetValue(Constants.SequreKeyForUserIdInDB));
            userId = Int32.Parse(id);
            userData = sQLiteRepository.GetItem(userId);
            MyIcon = userData.Photo;
            MyName = $"{userData.Name} {userData.Surname}";

            MenuItems = new List<MenuItem>
            {
                new MenuItem("Edit profile", this, typeof(UserDataFragmentViewModel)),
                new MenuItem("Main", this, typeof(MainFragmentViewModel)),
                new MenuItem("Log Out", this, typeof(LoginFragmentViewModel)),
            };
        }

        public List<MenuItem> MenuItems { get; private set; }

        public class MenuItem
        {
            public MenuItem(string title, MenuFragmentViewModel parent, Type viewModelUrl)
            {
                Title = title;

                ShowCommand = new MvxCommand(async () => await parent.NavigationService.Navigate(viewModelUrl));
            }

            public string Title { get; private set; }
            public IMvxCommand ShowCommand { get; private set; }
        }

        private string _myIcon;
        public string MyIcon
        {
            get => _myIcon;
            set
            {
                _myIcon = value;
                RaisePropertyChanged(() => MyIcon);
            }
        }

        public MvxAsyncCommand EditUserData
        {
            get
            {
                return new MvxAsyncCommand(async () =>
                {
                    await NavigationService.Navigate<UserDataFragmentViewModel>();
                });
            }
        }

        private string _myName;
        public string MyName
        {
            get => _myName;
            set
            {
                _myName = value;
                RaisePropertyChanged(() => MyName);
            }
        }
    }
}
