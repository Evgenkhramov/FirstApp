using Android.App;
using Android.OS;
using MvvmCross.Platforms.Android.Views;
using FirstApp.Core.ViewModels;
using Android.Widget;

namespace FirstApp.Droid.Views
{
    [Activity(Label = "First Application",  NoHistory = true, Theme = "@style/AppTheme")]
    public class TipView : MvxActivity<MainViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.MainView);
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(toolbar); 
        }
    }
}