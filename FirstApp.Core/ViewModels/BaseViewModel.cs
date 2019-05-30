using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace FirstApp.Core.ViewModels
{
    public abstract class BaseViewModel : MvxViewModel
    {
        #region Variables
        protected readonly IMvxNavigationService _navigationService;
        #endregion Variables

        #region Constructors

        protected BaseViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        #endregion Constructors
    }

    public abstract class BaseViewModel<TParameter, TResult> : MvxViewModel<TParameter, TResult>
        where TParameter : class
        where TResult : class
    {
        #region Variables
        protected readonly IMvxNavigationService _navigationService;
        #endregion Variables

        #region Constructors

        protected BaseViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        #endregion Constructors
    }

    public abstract class BaseViewModel<TParameter> : MvxViewModel<TParameter>
        where TParameter : class
    {
        #region Variables

        protected readonly IMvxNavigationService _navigationService;

        #endregion Variables

        #region Constructors

        protected BaseViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        #endregion Constructors
    }
}
