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
        #region Overrides

        protected override void OnCreate(Android.OS.Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.StartView);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);

            UserDialogs.Init(this);

            SetActionBar(toolbar);

            if (bundle == null)
            {
                ViewModel.ShowLoginFragmentCommand.Execute(null);
            }
        }

        #endregion Overrides
    }
}
