using Acr.UserDialogs;
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

        #region Variables

        private readonly IDBUserService _dBUserService;
        private int _userId;
        private string _id;
        private UserDatabaseModel userData;

        #endregion Variables

        #region Constructors

        public MenuViewModel(IDBUserService sQLiteRepository, IMvxNavigationService navigationService, IUserDialogs userDialogs) : base(navigationService, userDialogs)
        {
            _dBUserService = sQLiteRepository;

            _id = (CrossSecureStorage.Current.GetValue(Constants.SequreKeyForUserIdInDB));
            if (!string.IsNullOrEmpty(_id))
            {
                _userId = int.Parse(_id);
                userData = sQLiteRepository.GetItem(_userId);
                MyIcon = userData.Photo;
                MyName = $"{userData.Name} {userData.Surname}";
            }

            MenuItems = new MvxObservableCollection<MenuItem>
            {
                new MenuItem("Task List", this, typeof(TaskListViewModel)),
                new MenuItem("Log Out", this, typeof(LoginViewModel)),
            };
        }

        #endregion Constructors

        #region Properties

        private MvxObservableCollection<MenuItem> _menuItems;
        public MvxObservableCollection<MenuItem> MenuItems
        {
            get => _menuItems;
            set => SetProperty(ref _menuItems, value);
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

        #endregion Properties

        #region Commands  

        public MvxAsyncCommand<MenuItem> ItemClickedCommand
        {
            get
            {
                return new MvxAsyncCommand<MenuItem>(async (param) =>
                {
                    if (!(param.Title == Constants.LogOutUser))
                    {
                        await _navigationService.Navigate(param.ShowCommand);
                        return;
                    }

                    CrossSecureStorage.Current.DeleteKey(_id);
                    CrossSecureStorage.Current.SetValue(Constants.SequreKeyForLoged, Constants.LogOut);
                    _dBUserService.DeleteItem(_userId);
                    await _navigationService.Navigate(param.ShowCommand);
                });
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

        #endregion Commands

        public class MenuItem
        {
            public string Title { get; private set; }
            public Type ShowCommand { get; private set; }

            public MenuItem(string title, MenuViewModel parent, Type viewModelUrl)
            {
                Title = title;
                ShowCommand = viewModelUrl;
            }
        }
    }
}
