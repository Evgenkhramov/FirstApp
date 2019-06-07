using Android.OS;
using Android.Views;
using FirstApp.Core;
using FirstApp.Droid.Interfaces;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.ViewModels;
using System;
using System.Threading.Tasks;

namespace FirstApp.Droid.Views
{
    public abstract class BaseFragment<TMainView> : MvxFragment where TMainView : class, IMainView
    {
        #region Variables

        protected abstract int FragmentId { get; }

        #endregion Variables

        #region Methods

        public async Task CloseMenu()
        {
            TMainView mainActivity = Activity as TMainView;

            mainActivity.CloseDrawer();

            await Task.Delay(TimeSpan.FromMilliseconds(Constants.TimeOutLong));
        }

        public async Task OpenMenu()
        {
            TMainView mainActivity = Activity as TMainView;

            mainActivity.OpenDrawer();

            await Task.Delay(TimeSpan.FromMilliseconds(Constants.TimeOutLong));
        }

        #endregion Methods

        #region Overrides

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View ignore = base.OnCreateView(inflater, container, savedInstanceState);

            View view = this.BindingInflate(FragmentId, null);

            CloseMenu();

            return view;
        }

        #endregion Overrides
    }

    public abstract class BaseFragment<TMainView, TViewModel> : BaseFragment<TMainView> where TViewModel : class, IMvxViewModel where TMainView : class, IMainView
    {
        public new TViewModel ViewModel
        {
            get { return (TViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }
    }
}