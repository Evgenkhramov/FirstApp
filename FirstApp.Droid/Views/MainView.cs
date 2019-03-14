using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acr.UserDialogs;
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
    [Activity(Label = "MainView",
        Theme = "@style/AppTheme",
        NoHistory = true,
        LaunchMode = LaunchMode.SingleTask,
        Name = "FirstApp.Droid.Views.MainView"
        )]
    public class MainView : MvxAppCompatActivity<MainViewModel>
    {
        protected override void OnCreate(Android.OS.Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.MainView);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(toolbar);
            UserDialogs.Init(this);
            if (bundle == null)
            {
                ViewModel.ShowMainFragmentCommand.Execute(null);
            }
        }

        private void Initialize()
        {
        }
    }
}