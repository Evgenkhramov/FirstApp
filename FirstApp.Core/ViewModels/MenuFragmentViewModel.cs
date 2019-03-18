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
                new MenuItem("Erite User Data", this, nameof(UserDataFragmentViewModel)),
                new MenuItem("Log Out", this, nameof(LoginFragmentViewModel)),
             
            };
        }
  
        public List<MenuItem> MenuItems { get; private set; }

        public class MenuItem
        {
            public MenuItem(string title, MenuFragmentViewModel parent, string viewModelUrl)
            {
                Title = title;
                // Will change to navigate to type once https://github.com/MvvmCross/MvvmCross/pull/2148 is in.
                ShowCommand = new MvxCommand(async () => await parent.NavigationService.Navigate(viewModelUrl));
            }

            public string Title { get; private set; }
            public ICommand ShowCommand { get; private set; }
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
