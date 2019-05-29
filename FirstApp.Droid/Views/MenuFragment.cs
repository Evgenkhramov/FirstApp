using Android.OS;
using Android.Runtime;
using Android.Views;
using FirstApp.Core.ViewModels;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using System;
using System.Threading.Tasks;

namespace FirstApp.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.navigation_frame)]
    [Register("firstApp.Droid.Views.MenuFragment")]
    public class MenuFragment : BaseFragment<MenuViewModel>
    {
        #region Overrides

        protected override int FragmentId => Resource.Layout.MenuFragment;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);

            return view;
        }

        #endregion Overrides

        #region Methods

        public async Task CloseMenu()
        {
            ((MainView)Activity).DrawerLayout.CloseDrawers();

            await Task.Delay(TimeSpan.FromMilliseconds(250));
        }

        #endregion Methods
    }
}