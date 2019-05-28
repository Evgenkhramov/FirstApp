using Android.OS;
using Android.Support.V4.View;
using Android.Views;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.ViewModels;
using System;
using System.Threading.Tasks;

namespace FirstApp.Droid.Views
{
    public abstract class BaseFragment : MvxFragment
    {
        #region Variables

        protected abstract int FragmentId { get; }

        #endregion Variables

        #region Methods

        public async Task CloseMenu()
        {
            ((MainView)Activity).DrawerLayout.CloseDrawers();

            await Task.Delay(TimeSpan.FromMilliseconds(250));
        }

        public async Task OpenMenu()
        {
            ((MainView)Activity).DrawerLayout.OpenDrawer(GravityCompat.Start);

            await Task.Delay(TimeSpan.FromMilliseconds(250));
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

    public abstract class BaseFragment<TViewModel> : BaseFragment where TViewModel : class, IMvxViewModel
    {
        public new TViewModel ViewModel
        {
            get { return (TViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }
    }
}
