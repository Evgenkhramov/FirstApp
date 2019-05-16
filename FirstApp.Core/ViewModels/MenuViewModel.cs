using FirstApp.Core.Interfaces;
using FirstApp.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.SecureStorage;
using System;

namespace FirstApp.Core.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private readonly IDBUserService _sqliteRepository;
        private int userId;
        private string id;
        private UserDatabaseModel userData;

        private MvxObservableCollection<MenuItem> _menuItems;
        public MvxObservableCollection<MenuItem> MenuItems
        {
            get => _menuItems;
            set => SetProperty(ref _menuItems, value);
        }

        public MenuViewModel(IDBUserService sQLiteRepository, IMvxNavigationService navigationService) : base(navigationService)
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
                new MenuItem("Task List", this, typeof(TaskListViewModel)),
                new MenuItem("Log Out", this, typeof(LoginViewModel)),
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
                    await _navigationService.Navigate(param.ShowCommand);
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
                    await _navigationService.Navigate<UserDataViewModel>();
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
            public MenuItem(string title, MenuViewModel parent, Type viewModelUrl)
            {
                Title = title;
                ShowCommand = viewModelUrl;
            }
            public string Title { get; private set; }
            public Type ShowCommand { get; private set; }
        }
    }
}
