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
        private readonly IMvxPictureChooserTask _pictureChooserTask;
        private readonly IDBUserService _sQLiteRepository;
        private UserDatabaseModel _userData;
        private int _userId;
        public UserDataViewModel(IDBUserService sQLiteRepository, IMvxPictureChooserTask pictureChooserTask, IMvxNavigationService navigationService) : base(navigationService)
        {
            try
            {
                _pictureChooserTask = pictureChooserTask;
                _sQLiteRepository = sQLiteRepository;     
                string id = (CrossSecureStorage.Current.GetValue(Constants.SequreKeyForUserIdInDB));
                _userId = Int32.Parse(id);
                _userData = sQLiteRepository.GetItem(_userId);
                MyPhoto = _userData.Photo;
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
            get => _userName = _userData.Name;
            set
            {
                _userName = value;
                _userData.Name = _userName;
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
            get => _myPhoto;
            set
            {
                _myPhoto = value;

                RaisePropertyChanged(() => MyPhoto);
            }
        }

        private string _surname;
        public string Surname
        {
            get => _surname = _userData.Surname;
            set
            {
                _surname = value;
                _userData.Surname = _surname;
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
                    await _navigationService.Navigate<TaskListViewModel>();
                });
            }
        }

        public MvxAsyncCommand SaveUserDataiOS
        {
            get
            {
                return new MvxAsyncCommand(async () =>
                {
                    _userData.Photo = MyPhoto;
                    _userData.Id = _userId;
                    _sQLiteRepository.SaveItem(_userData);                   
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
                    Surname = _userData.Surname;
                    UserName = _userData.Name;
                    await _navigationService.Navigate<UserDataViewModel>();
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

