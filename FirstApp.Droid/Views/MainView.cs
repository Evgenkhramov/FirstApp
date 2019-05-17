using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Views.InputMethods;
using FirstApp.Core.ViewModels;
using FirstApp.Droid.Interfaces;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace FirstApp.Droid.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "MainView",
        Theme = "@style/AppTheme",
        NoHistory = false,
        LaunchMode = LaunchMode.SingleTask,
        Name = "FirstApp.Droid.Views.MainView"
        )]
    public class MainView : MvxAppCompatActivity<MainViewModel>
    {
        public DrawerLayout DrawerLayout { get; set; }
        protected override void OnCreate(Android.OS.Bundle bundle)
        {
            base.OnCreate(bundle);
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, bundle);
            SetContentView(Resource.Layout.MainView);
            UserDialogs.Init(this);

            DrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            if (bundle == null)
            {
                ViewModel.ShowMain();
            }
        }

        public void OpenDraweble()
        {
            DrawerLayout.OpenDrawer(GravityCompat.Start);
        }

        public override void OnBackPressed()
        {
            if (DrawerLayout != null && DrawerLayout.IsDrawerOpen(GravityCompat.Start))
            {
                DrawerLayout.CloseDrawers();
            }
            else
            {
                Android.Support.V4.App.Fragment currentFragment = SupportFragmentManager.FindFragmentById(Resource.Id.content_frame_new);
                IBackButtonListener listener = currentFragment as IBackButtonListener;
                if (listener != null)
                {
                    listener.OnBackPressed();
                    return;
                }
                CloseApplication();
            }
        }
        public void HideSoftKeyboard()
        {
            if (CurrentFocus == null)
            {
                return;
            }
               
            InputMethodManager inputMethodManager = (InputMethodManager)GetSystemService(InputMethodService);
            inputMethodManager.HideSoftInputFromWindow(CurrentFocus.WindowToken, 0);
            CurrentFocus.ClearFocus();
        }

        public void CloseApplication()
        {
            var activity = (Activity)this;
            activity.FinishAffinity();
        }
    }
}
