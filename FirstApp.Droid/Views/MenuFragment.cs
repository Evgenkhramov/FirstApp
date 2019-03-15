using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using FirstApp.Core.ViewModels;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace FirstApp.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.navigation_frame)]
    [Register("starWarsSample.droid.views.MenuView")]
    public class MenuFragment : MvxFragment<MenuFragmentViewModel>, NavigationView.IOnNavigationItemSelectedListener
    {
        private NavigationView _navigationView;
        private IMenuItem _previousMenuItem;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);

            var view = this.BindingInflate(Resource.Layout.MenuFragment, null);

            _navigationView = view.FindViewById<NavigationView>(Resource.Id.navigation_view);
            _navigationView.SetNavigationItemSelectedListener(this);
            var recyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.menu_recycler_view);
            if (recyclerView != null)
            {
                recyclerView.HasFixedSize = true;
                var layoutManager = new LinearLayoutManager(Activity);
                recyclerView.SetLayoutManager(layoutManager);

                //recyclerView.AddOnScrollFetchItemsListener(layoutManager, () => ViewModel.FetchPeopleTask, () => this.ViewModel.FetchPeopleCommand);
            }
            return view;
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            if (_previousMenuItem != null)
                _previousMenuItem.SetChecked(false);

            item.SetCheckable(true);
            item.SetChecked(true);

            _previousMenuItem = item;

            //Navigate(item.ItemId);

            return true;
        }

        public async Task CloseMenu()
        {
            ((MainView)Activity).DrawerLayout.CloseDrawers();
            await Task.Delay(TimeSpan.FromMilliseconds(250));
        }

        //private async Task Navigate(int itemId)
        //{
        //    ((MainView)Activity).DrawerLayout.CloseDrawers();
        //    await Task.Delay(TimeSpan.FromMilliseconds(250));

        //    switch (itemId)
        //    {
        //        case Resource.Id.nav_logout:
        //            ViewModel.ShowLoginCommand.Execute();
        //            break;
            
        //    }
        //}
    }
}