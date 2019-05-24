using Acr.UserDialogs;
using FirstApp.Core.Interfaces;
using FirstApp.Core.Models;
using MvvmCross;
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
        private readonly IMvxPictureChooserTask _pictureChooserTask;
        private readonly IDBUserService _sQLiteRepository;
        private UserDatabaseModel _userData;
        private int _userId;
        private readonly IGetCurrentPlatformService _getCurrentPlatform;

        public UserDataViewModel(IGetCurrentPlatformService getCurrentPlatform, IDBUserService sQLiteRepository, IMvxPictureChooserTask pictureChooserTask, IMvxNavigationService navigationService) : base(navigationService)
        {
            try
            {
                _getCurrentPlatform = getCurrentPlatform;
                _pictureChooserTask = pictureChooserTask;
                _sQLiteRepository = sQLiteRepository;
                string id = (CrossSecureStorage.Current.GetValue(Constants.SequreKeyForUserIdInDB));
                _userId = Int32.Parse(id);
                _userData = sQLiteRepository.GetItem(_userId);
                MyPhoto = _userData.Photo;
                UserName = _userData.Name;
                Surname = _userData.Surname;
            }
            catch (Exception ex)
            {
                ;
            }
        }

        public IMvxAsyncCommand ShowMenuViewModelCommand { get; private set; }

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

        public void SavePhoto(string photo)
        {
            _userData.Id = _userId;
            _userData.Photo = photo;
            MyPhoto = _userData.Photo;
            _sQLiteRepository.SaveItem(_userData);
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

        public MvxAsyncCommand SaveUserData
        {
            get
            {
                return new MvxAsyncCommand(async () =>
                {
                    _userData.Photo = MyPhoto;
                    _userData.Id = _userId;
                    _sQLiteRepository.SaveItem(_userData);
                    var platform = _getCurrentPlatform.CurrentPlatform();
                    if (platform == Enums.CurrentPlatform.Android)
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
                    _userData = _sQLiteRepository.GetItem(_userId);
                    MyPhoto = _userData.Photo;
                    Surname = _userData.Surname;
                    UserName = _userData.Name;
                    var platform = _getCurrentPlatform.CurrentPlatform();

                    if (platform == Enums.CurrentPlatform.Android)
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
                    bool answ = await Mvx.IoCProvider.Resolve<IUserDialogs>().ConfirmAsync(Constants.WantLogOut, Constants.WLogOut, Constants.Yes, Constants.No);
                    if (answ)
                    {
                        CrossSecureStorage.Current.DeleteKey(Constants.SequreKeyForUserIdInDB);
                        CrossSecureStorage.Current.DeleteKey(Constants.SequreKeyForUserName);
                        CrossSecureStorage.Current.DeleteKey(Constants.SequreKeyForUserPassword);
                        CrossSecureStorage.Current.SetValue(Constants.SequreKeyForLoged, Constants.LogOut);

                        _sQLiteRepository.DeleteItem(_userId);

                        await _navigationService.Navigate<LoginViewModel>();
                    }
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

        private void DoTakePicture()
        {
            _pictureChooserTask.TakePicture(400, 95, OnPicture, () => { });
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

        private void DoChoosePicture()
        {
            _pictureChooserTask.ChoosePictureFromLibrary(400, 95, OnPicture, () => { });
        }

        private byte[] _bytes;

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
    }
}

