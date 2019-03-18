using FirstApp.Core.Models;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace FirstApp.Core.ViewModels
{
    public class MenuFragmentViewModel : BaseViewModel
    {
        public MenuFragmentViewModel()
        {
            ShowLoginCommand = LogOut;
            MenuItems = new List<MenuItem>
            {
                new MenuItem("Edit profile", this, typeof(UserDataFragmentViewModel)),   
                new MenuItem("Main", this, typeof(MainFragmentViewModel)),
                new MenuItem("Log Out", this, typeof(LoginFragmentViewModel)),
            };
        }
  
        public List<MenuItem> MenuItems { get; private set; }

        public class MenuItem
        {
            public MenuItem(string title, MenuFragmentViewModel parent, Type viewModelUrl)
            {
                Title = title;
               
                ShowCommand = new MvxCommand(async () => await parent.NavigationService.Navigate(viewModelUrl));
            }

            public string Title { get; private set; }
            public IMvxCommand ShowCommand { get; private set; }
        }

        public MvxCommand LogOut
        {
            get
            {
                return new MvxCommand(async () =>
                {
                    CrossSecureStorage.Current.SetValue(Constants.SequreKeyForLoged, Constants.LogOut);

                    await NavigationService.Navigate<LoginFragmentViewModel>();
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
                    
                    await NavigationService.Navigate<UserDataFragmentViewModel>();
                });
            }
        }

        // MvvmCross Lifecycle

        // MVVM Properties

        // MVVM Commands


        // Private methods
    }
}
