using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace FirstApp.Core.ViewModels
{
    public abstract class BaseViewModel : MvxViewModel
    {
        protected readonly IMvxNavigationService _navigationService;
        protected BaseViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }

    public abstract class BaseViewModel<TParameter, TResult> : MvxViewModel<TParameter, TResult>
        where TParameter : class
        where TResult : class
    {
        protected readonly IMvxNavigationService _navigationService;
        protected BaseViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }

    public abstract class BaseViewModel<TParameter> : MvxViewModel<TParameter>
        where TParameter : class
    {
        public IMvxNavigationService _navigationService;
        protected BaseViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
