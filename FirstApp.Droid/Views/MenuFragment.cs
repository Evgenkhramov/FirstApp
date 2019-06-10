using Android.OS;
using Android.Runtime;
using Android.Views;
using FirstApp.Core.ViewModels;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace FirstApp.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.navigation_frame)]
    [Register("firstApp.Droid.Views.MenuFragment")]
    public class MenuFragment : BaseFragment<MainView,MenuViewModel>
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

        private void MenuDrawerOpened(object sender, Android.Support.V4.Widget.DrawerLayout.DrawerOpenedEventArgs menuEvent)
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

        public override void OnDestroyView()
        {
            _menu.DrawerOpened -= MenuDrawerOpened;

            base.OnDestroyView();
        }
        #endregion Methods
    }
}