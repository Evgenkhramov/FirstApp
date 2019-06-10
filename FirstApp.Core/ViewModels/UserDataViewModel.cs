using Acr.UserDialogs;
using FirstApp.Core.Entities;
using FirstApp.Core.Interfaces;
using FirstApp.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.PictureChooser;
using Plugin.SecureStorage;
using System;
using System.IO;

namespace FirstApp.Core.ViewModels
{
    public class UserDataViewModel : BaseViewModel
    {
        #region Variables

        private readonly IMvxPictureChooserTask _pictureChooserTask;
        private readonly IUserService _userService;
        private UserDatabaseEntity _userData;
        private readonly int _userId;
        private readonly ICurrentPlatformService _currentPlatformService;

        #endregion Variables

        #region Constructors

        public UserDataViewModel(ICurrentPlatformService currentPlatformService, IUserService dBUserService,
              IMvxPictureChooserTask pictureChooserTask, IMvxNavigationService navigationService,
              IUserDialogs userDialogs) : base(navigationService, userDialogs)
        {
            _currentPlatformService = currentPlatformService;
            _pictureChooserTask = pictureChooserTask;
            _userService = dBUserService;

            _userId = int.Parse(CrossSecureStorage.Current.GetValue(Constants.SequreKeyForUserIdInDB));
            _userData = dBUserService.GetItem(_userId);

            GetUserData();         
        }

        #endregion ConstructorsE

        #region Properties

        private string _surname;
        public string Surname
        {
            get
            {
                return _surname;
            }
            set
            {
                _surname = value;
                RaisePropertyChanged(() => Surname);
            }
        }

        private string _myPhoto;
        public string MyPhoto
        {
            get
            {
                return _myPhoto;
            }
            set
            {
                _myPhoto = value;

                RaisePropertyChanged(() => MyPhoto);
            }
        }

        private string _userName;
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                RaisePropertyChanged(() => UserName);
            }
        }

        #endregion Properties

        #region Commands  

        public IMvxAsyncCommand ShowMenuViewModelCommand { get; private set; }

        public MvxAsyncCommand SaveUserData
        {
            get
            {
                return new MvxAsyncCommand(async () =>
                {
                    _userData.Photo = MyPhoto;
                    _userData.Id = _userId;
                    _userData.Name = UserName;
                    _userData.Surname = Surname;

                    _userService.SaveItem(_userData);

                    if (_currentPlatformService.GetCurrentPlatform() != CurrentPlatformType.Android)
                    {
                        return;
                    }

                    await _navigationService.Close(this);
                    await _navigationService.Navigate<TaskListViewModel>();
                });
            }
        }

        public MvxAsyncCommand Cancel
        {
            get
            {
                return new MvxAsyncCommand(async () =>
                {
                    _userData = _userService.GetItem(_userId);

                    MyPhoto = _userData.Photo;
                    Surname = _userData.Surname;
                    UserName = _userData.Name;

                    if (_currentPlatformService.GetCurrentPlatform() == CurrentPlatformType.Android)
                    {
                        await _navigationService.Navigate<TaskListViewModel>();
                    }
                });
            }
        }

        public MvxAsyncCommand LogOutCommand
        {
            get
            {
                return new MvxAsyncCommand(async () =>
                {
                    bool IsUserAccept = await _userDialogs.ConfirmAsync(Constants.WantLogOut, Constants.WLogOut, Constants.Yes, Constants.No);

                    if (!IsUserAccept)
                    {
                        return;
                    }

                    CrossSecureStorage.Current.DeleteKey(Constants.SequreKeyForUserIdInDB);
                    CrossSecureStorage.Current.SetValue(Constants.SequreKeyForLoged, Constants.LogOut);

                    await _navigationService.Close(this);
                    await _navigationService.Navigate<LoginViewModel>();
                });
            }
        }

        public MvxAsyncCommand CloseFragment
        {
            get
            {
                return new MvxAsyncCommand(async () =>
                {
                    await _navigationService.Close(this);
                    await _navigationService.Navigate<TaskListViewModel>();
                });
            }
        }

        private MvxCommand _takePictureCommand;

        public System.Windows.Input.ICommand TakePictureCommand
        {
            get
            {
                _takePictureCommand = _takePictureCommand ?? new MvxCommand(DoTakePicture);
                return _takePictureCommand;
            }
        }

        private MvxCommand _choosePictureCommand;

        public IMvxCommand ChoosePictureCommand
        {
            get
            {
                _choosePictureCommand = _choosePictureCommand ?? new MvxCommand(DoChoosePicture);
                return _choosePictureCommand;
            }
        }

        #endregion Commands

        #region Methods

        public void GetUserData()
        {
            if (!string.IsNullOrEmpty(_userData.Photo))
            {
                MyPhoto = _userData.Photo;
            }

            if (!string.IsNullOrEmpty(_userData.Name))
            {
                UserName = _userData.Name;
            }

            if (!string.IsNullOrEmpty(_userData.Surname))
            {
                Surname = _userData.Surname;
            }
        }

        public void SavePhoto(string photo)
        {
            _userData.Id = _userId;
            _userData.Photo = photo;
            MyPhoto = _userData.Photo;

            _userService.SaveItem(_userData);
        }

        private void DoTakePicture()
        {
            _pictureChooserTask.TakePicture(400, 95, OnPicture, () => { });
        }

        private void DoChoosePicture()
        {
            _pictureChooserTask.ChoosePictureFromLibrary(400, 95, OnPicture, () => { });
        }

        private void OnPicture(Stream pictureStream)
        {
            var bytes = new byte[default(int)];

            pictureStream.Write(bytes, 0, (int)pictureStream.Length);

            MyPhoto = Convert.ToBase64String(bytes);
        }

        #endregion Methods
    }
}

