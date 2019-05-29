using Acr.UserDialogs;
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
        public Action TempAction;
        private readonly IMvxPictureChooserTask _pictureChooserTask;
        private readonly IDBUserService _dBUserService;
        private UserDatabaseModel _userData;
        private int _userId;
        private readonly ICurrentPlatformService _getCurrentPlatform;
        private byte[] _bytes;

        #endregion Variables

        #region Constructors

        public UserDataViewModel(ICurrentPlatformService getCurrentPlatform, IDBUserService dBUserService,
              IMvxPictureChooserTask pictureChooserTask, IMvxNavigationService navigationService,
              IUserDialogs userDialogs) : base(navigationService, userDialogs)
        {
            _getCurrentPlatform = getCurrentPlatform;
            _pictureChooserTask = pictureChooserTask;
            _dBUserService = dBUserService;
            string id = (CrossSecureStorage.Current.GetValue(Constants.SequreKeyForUserIdInDB));
            _userId = Int32.Parse(id);
            _userData = dBUserService.GetItem(_userId);
            MyPhoto = _userData.Photo;
            UserName = _userData.Name;
            Surname = _userData.Surname;
        }

        #endregion Constructors

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
                    _dBUserService.SaveItem(_userData);
                    var platform = _getCurrentPlatform.GetCurrentPlatform();
                    if (platform == CurrentPlatformType.Android)
                    {
                        await _navigationService.Navigate<TaskListViewModel>();
                    }
                });
            }
        }

        public MvxAsyncCommand Cancel
        {
            get
            {
                return new MvxAsyncCommand(async () =>
                {
                    _userData = _dBUserService.GetItem(_userId);

                    MyPhoto = _userData.Photo;
                    Surname = _userData.Surname;
                    UserName = _userData.Name;
                    var platform = _getCurrentPlatform.GetCurrentPlatform();

                    if (platform == CurrentPlatformType.Android)
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
                    bool answ = await _userDialogs.ConfirmAsync(Constants.WantLogOut, Constants.WLogOut, Constants.Yes, Constants.No);
                    if (!answ)
                    {
                        return;
                    }

                    CrossSecureStorage.Current.DeleteKey(Constants.SequreKeyForUserIdInDB);
                    CrossSecureStorage.Current.DeleteKey(Constants.SequreKeyForUserName);
                    CrossSecureStorage.Current.DeleteKey(Constants.SequreKeyForUserPassword);
                    CrossSecureStorage.Current.SetValue(Constants.SequreKeyForLoged, Constants.LogOut);

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

        public void SavePhoto(string photo)
        {
            _userData.Id = _userId;
            _userData.Photo = photo;
            MyPhoto = _userData.Photo;
            _dBUserService.SaveItem(_userData);
        }

        private void DoTakePicture()
        {
            _pictureChooserTask.TakePicture(400, 95, OnPicture, () => { });
        }

        private void DoChoosePicture()
        {
            _pictureChooserTask.ChoosePictureFromLibrary(400, 95, OnPicture, () => { });
        }

        public byte[] Bytes
        {
            get { return _bytes; }
            set { _bytes = value; RaisePropertyChanged(() => Bytes); }
        }

        private void OnPicture(Stream pictureStream)
        {
            var memoryStream = new MemoryStream();
            pictureStream.CopyTo(memoryStream);
            Bytes = memoryStream.ToArray();
            MyPhoto = Convert.ToBase64String(Bytes);
        }

        #endregion Methods
    }
}

