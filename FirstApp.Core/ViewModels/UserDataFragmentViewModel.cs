using FirstApp.Core.Interfaces;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstApp.Core.ViewModels
{
    public class UserDataFragmentViewModel : BaseViewModel
    {
        private readonly ISQLiteRepository _sQLiteRepository;

        public UserDataFragmentViewModel(ISQLiteRepository sQLiteRepository)
        {

            _sQLiteRepository = sQLiteRepository;
         
            HaveGone = false;
            ShowMenuViewModelCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<MenuFragmentViewModel>());

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
