using FirstApp.Core.Interfaces;
using FirstApp.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstApp.Core.ViewModels
{
    public class UserDataFragmentViewModel : BaseViewModel
    {
        private readonly ISQLiteRepository _sQLiteRepository;
        private readonly IRegistrationService _registrationService;
        private UserDatabaseModel userData;
        private int userId;
        public UserDataFragmentViewModel(ISQLiteRepository sQLiteRepository, IRegistrationService registrationService)
        {
            _registrationService = registrationService;
            _sQLiteRepository = sQLiteRepository;

            HaveGone = false;
            ShowMenuViewModelCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<MenuFragmentViewModel>());
            string id = (CrossSecureStorage.Current.GetValue(Constants.SequreKeyForUserIdInDB));
            userId = Int32.Parse(id);
            userData = sQLiteRepository.GetItem(userId);
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
                    await NavigationService.Navigate<MainFragmentViewModel>();
                });
             }
        }
    }
}
