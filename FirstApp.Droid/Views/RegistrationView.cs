using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FirstApp.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace FirstApp.Droid.Views
{

   [MvxActivityPresentation]
   [Activity(Label = "Registration",
   Theme = "@style/AppTheme",
   LaunchMode = LaunchMode.SingleTop,
   Name = "FirstApp.Droid.Views.Registration"
   )]
    public class Registration : MvxAppCompatActivity<RegistrationViewModel>
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Registration);
        }

        private void Initialize()
        {
        }
    }

}