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
        private readonly ISQLiteRepository _sqliteRepository;
        private int userId;
        private string id;
        private UserDatabaseModel userData;

        private MvxObservableCollection<MenuItem> _menuItems;
        public MvxObservableCollection<MenuItem> MenuItems
        {
            get => _menuItems;
            set => SetProperty(ref _menuItems, value);
        }

        public MenuFragmentViewModel(ISQLiteRepository sQLiteRepository)
        {
            _sqliteRepository = sQLiteRepository;

            id = (CrossSecureStorage.Current.GetValue(Constants.SequreKeyForUserIdInDB));
            if (!string.IsNullOrEmpty(id))
            {
                userId = Int32.Parse(id);
                userData = sQLiteRepository.GetItem(userId);
                MyIcon = userData.Photo;

                MyName = $"{userData.Name} {userData.Surname}";
            }

            MenuItems = new MvxObservableCollection<MenuItem>
            {
                new MenuItem("Edit profile", this, typeof(UserDataFragmentViewModel)),
                new MenuItem("Main", this, typeof(MainFragmentViewModel)),
                new MenuItem("Log Out", this, typeof(LoginFragmentViewModel)),
            };
        }

        public MvxAsyncCommand<MenuItem> ItemClickedCommand
        {
            get
            {
                return new MvxAsyncCommand<MenuItem>(async (param) =>
                {
                    if (param.Title == "Log Out")
                    {
                        CrossSecureStorage.Current.DeleteKey(id);
                        CrossSecureStorage.Current.SetValue(Constants.SequreKeyForLoged, Constants.LogOut);
                        _sqliteRepository.DeleteItem(userId);
                    }
                    await NavigationService.Navigate(param.ShowCommand);
                });
            }
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

        public class MenuItem
        {
            public MenuItem(string title, MenuFragmentViewModel parent, Type viewModelUrl)
            {
                Title = title;
                ShowCommand = viewModelUrl;
            }
            public string Title { get; private set; }
            public Type ShowCommand { get; private set; }
        }
    }
}
