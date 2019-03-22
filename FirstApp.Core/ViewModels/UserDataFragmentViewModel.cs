using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Widget;
using FirstApp.Core.Interfaces;
using FirstApp.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Converters;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace FirstApp.Core.ViewModels
{
    public class UserDataFragmentViewModel : BaseViewModel
    {
        private readonly ISQLiteRepository _sQLiteRepository;
        private readonly IRegistrationService _registrationService;
        private readonly IUserDialogService _userDialogService;

        private UserDatabaseModel userData;
        private int userId;
        public UserDataFragmentViewModel(ISQLiteRepository sQLiteRepository, IRegistrationService registrationService, IUserDialogService userDialogService)
        {
            _userDialogService = userDialogService;
            _registrationService = registrationService;
            _sQLiteRepository = sQLiteRepository;

            HaveGone = false;
            ShowMenuViewModelCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<MenuFragmentViewModel>());
            string id = (CrossSecureStorage.Current.GetValue(Constants.SequreKeyForUserIdInDB));
            userId = Int32.Parse(id);
            userData = sQLiteRepository.GetItem(userId);
            MyPhoto = userData.Photo;
        }

        public IMvxAsyncCommand ShowMenuViewModelCommand { get; private set; }

        private bool _haveGone;
        public bool HaveGone
        {
            get => _haveGone;
            set
            {
                _haveGone = value;
            }
        }

        private string _userName;
        public string UserName
        {
            get => _userName = userData.Name;
            set
            {
                _userName = value;
                userData.Name = _userName;
            }
        }


        public byte[] BitmapToByte(Bitmap bitmap)
        {
            byte[] bitmapData;
            using (var stream = new MemoryStream())
            {
                bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                bitmapData = stream.ToArray();
            }
            return bitmapData;
        }

        public void SavePhoto(string photo)
        {
            userData.Id = userId;
            userData.Photo = photo;
            MyPhoto = userData.Photo;
            _sQLiteRepository.SaveItem(userData);

        }

        public class InMemoryImageConverter : MvxValueConverter<byte[], Bitmap>
        {
            protected override Bitmap Convert(byte[] value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value == null || value.Length == 0) { return null; }
                return BitmapFactory.DecodeByteArray(value, 0, value.Length);
            }

            protected override byte[] ConvertBack(Bitmap value, Type targetType, object parameter, CultureInfo culture)
            {
                var stream = new MemoryStream();
                value.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
                return stream.ToArray();
            }
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
            get => _surname = userData.Surname;
            set
            {
                _surname = value;
                userData.Surname = _surname;
            }
        }

        public MvxAsyncCommand SaveUserData
        {
            get
            {
                return new MvxAsyncCommand(async () =>
                {
                    userData.Id = userId;
                    _sQLiteRepository.SaveItem(userData);
                    await NavigationService.Navigate<MenuFragmentViewModel>();
                    await NavigationService.Navigate<MainFragmentViewModel>();
                });
             }
        }

        public MvxAsyncCommand Cancel
        {
            get
            {
                return new MvxAsyncCommand(async () =>
                {
                    userData = _sQLiteRepository.GetItem(userId);
                    Surname = userData.Surname;
                    UserName = userData.Name;
                    await NavigationService.Navigate<UserDataFragmentViewModel>();
                });
            }
        }

    }
}
