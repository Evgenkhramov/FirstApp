using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstApp.Core.ViewModels
{
    public abstract class BaseViewModel : MvxViewModel
    {
        protected readonly IMvxNavigationService NavigationService;
        protected BaseViewModel()
        {
            NavigationService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();           
        }

        public MvxAsyncCommand RegistrationBack
        {
            get
            {
                return new MvxAsyncCommand(async () =>
                {
                    await NavigationService.Navigate<MainViewModel>();
                });
            }
        }
    }

    public abstract class BaseViewModel<TParameter, TResult> : MvxViewModel<TParameter, TResult>
        where TParameter : class
        where TResult : class
    {
        protected readonly IMvxNavigationService NavigationService;
        protected BaseViewModel()
        {
            NavigationService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();
        }
    }
}
