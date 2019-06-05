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

        #region Variables

        private Android.Support.V4.Widget.DrawerLayout _menu;

        #endregion Variables

        #region Overrides

        protected override int FragmentId => Resource.Layout.MenuFragment;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);

            CloseMenu();

            _menu = ((MainView)Activity).DrawerLayout;

            _menu.DrawerOpened += MenuDrawerOpened;

            return view;
        }

        #endregion Overrides

        #region Methods

        public async Task CloseMenu()
        {
            ((MainView)Activity).DrawerLayout.CloseDrawers();

            await Task.Delay(TimeSpan.FromMilliseconds(250));
        }

        private void MenuDrawerOpened(object sender, Android.Support.V4.Widget.DrawerLayout.DrawerOpenedEventArgs e)
        {
            ViewModel.UpdateData();
        }

        public override void OnPause()
        {
            base.OnPause();
        }
        public override void OnResume()
        {
            base.OnResume();
        }

        #endregion Methods
    }
}