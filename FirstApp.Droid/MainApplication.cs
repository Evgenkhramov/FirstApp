using System;
using Android.App;
using Android.Runtime;
using FirstApp.Core;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace FirstApp.Droid
{
    [Application]
    public class MainApplication : MvxAppCompatApplication<Setup, App>
    {
        public MainApplication(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }
    }
}