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
    public class MenuFragmentViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public MenuFragmentViewModel(IMvxNavigationService navigationService)
        {
            MenuItem = new MvxObservableCollection<Item>();
            _navigationService = navigationService;
            ShowLoginCommand = LogOut;

        } 
              
        private MvxObservableCollection<Item> _menuItem;
        public MvxObservableCollection<Item> MenuItem
        {
            get
            {
                return _menuItem;
            }
            set
            {
                _menuItem = value;
                RaisePropertyChanged(() => MenuItem);
            }
        }
        public MvxCommand LogOut
        {
            get
            {
                return new MvxCommand(async () =>
                {
                    CrossSecureStorage.Current.SetValue(Constants.SequreKeyForLoged, Constants.LogOut);

                    await _navigationService.Navigate<LoginFragmentViewModel>();
                });
            }
        }
        public IMvxCommand ShowLoginCommand { get; private set; }

        public MvxCommand EditUserDate
        {
            get
            {
                return new MvxCommand(async () =>
                {
                    
                    await _navigationService.Navigate<UserDataFragmentViewModel>();
                });
            }
        }

        // MvvmCross Lifecycle

        // MVVM Properties

        // MVVM Commands


        // Private methods
    }
}
