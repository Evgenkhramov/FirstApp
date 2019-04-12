using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Views;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.ViewModels;
using Android.Widget;

namespace FirstApp.Droid.Views
{
   public abstract class BaseFragment : MvxFragment
    {
        //private Android.Support.V7.Widget.Toolbar _mainToolbar;
       
        public MvxAppCompatActivity ParentActivity
        {
            get
            {
                return (MvxAppCompatActivity)Activity;
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            
            var view = this.BindingInflate(FragmentId, null);

           //_mainToolbar = view.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);

            CloseMenu();
            return view;
        }

        protected abstract int FragmentId { get; }
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
