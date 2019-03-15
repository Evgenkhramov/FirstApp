using FirstApp.Core.Interfaces;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstApp.Core.ViewModels
{
    public class UserDataFragmentViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly ISQLiteRepository _sQLiteRepository;

        public UserDataFragmentViewModel(IMvxNavigationService navigationService, ISQLiteRepository sQLiteRepository)
        {

            _sQLiteRepository = sQLiteRepository;
            _navigationService = navigationService;
           
            HaveGone = false;
            ShowMenuViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<MenuFragmentViewModel>());

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


    }
}
