using Android.App;
using Android.OS;
using MvvmCross.Platforms.Android.Views;
using FirstApp.Core.ViewModels;

namespace FirstApp.Droid.Views
{
    [Activity(Label = "Tip Calculator", MainLauncher = true)]
    public class TipView : MvxActivity<MainViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.MainView);
        }
    }
}