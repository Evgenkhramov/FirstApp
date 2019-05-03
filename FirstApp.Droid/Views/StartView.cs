using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.Widget;
using FirstApp.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace FirstApp.Droid.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "StartView",
        Theme = "@style/AppTheme",
        NoHistory = true,
        LaunchMode = LaunchMode.SingleTask,
        Name = "FirstApp.Droid.Views.StartView"
        )]
    public class StartView : MvxAppCompatActivity<StartViewModel>
    {
        protected override void OnCreate(Android.OS.Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.StartView);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(toolbar);
            UserDialogs.Init(this);
            if (bundle == null)
            {
                ViewModel.ShowLoginFragmentCommand.Execute(null);             
            }
        }

        private void Initialize()
        {
        }
    }
}
