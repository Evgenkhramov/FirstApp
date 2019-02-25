using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using FirstApp.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace FirstApp.Droid.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "Login",
       Theme = "@style/AppTheme",
       LaunchMode = LaunchMode.SingleTop,
       Name = "FirstApp.Droid.Views.LoginView"
       )]
    public class LoginView : MvxAppCompatActivity<LoginViewModel>
    {

        protected override void OnCreate(Android.OS.Bundle bundle)
        {
            base.OnCreate(bundle);



            SetContentView(Resource.Layout.LoginView);

        }

        private void Initialize()
        {
        }
    }
}