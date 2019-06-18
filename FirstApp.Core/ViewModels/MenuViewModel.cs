using Acr.UserDialogs;
using FirstApp.Core.Entities;
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

        private readonly IUserService _userService;
        private readonly int _userId;
        private UserEntity _userData;

        #endregion Variables

        #region Constructors

        public MenuViewModel(IUserService userService, IMvxNavigationService navigationService, IUserDialogs userDialogs) : base(navigationService, userDialogs)
        {
            _userService = userService;

            _userId = Convert.ToInt32(CrossSecureStorage.Current.GetValue(Constants.SequreKeyForUserIdInDB));

            _userData = _userService.GetItem(_userId);

            MyIcon = _userData.Photo;

            MyName = $"{_userData.Name} {_userData.Surname}";

            MenuItems = new MvxObservableCollection<MenuItem>
            {
                new MenuItem(Constants.TaskList, typeof(TaskListViewModel)),
                new MenuItem(Constants.LogOutUser, typeof(LoginViewModel)),
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

                    CrossSecureStorage.Current.DeleteKey(_userId.ToString());
                    CrossSecureStorage.Current.SetValue(Constants.SequreKeyForLoged, Constants.LogOut);

                    _userService.DeleteItem(_userId);

                    await _navigationService.Close(this);
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

        public void UpdateData()
        {
            _userData = _userService.GetItem(_userId);

            MyIcon = _userData.Photo;
            MyName = $"{_userData.Name} {_userData.Surname}";
        }
    }
}
