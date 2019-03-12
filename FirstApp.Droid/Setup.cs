using System.Collections.Generic;
using System.Reflection;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;

using MvvmCross.Platforms.Android.Presenters;
using FirstApp.Core;
using FirstApp.Core.Interfaces;
using MvvmCross.IoC;
using MvvmCross;
using FirstApp.Droid.Services;

namespace FirstApp.Droid
{
    public class Setup : MvxAppCompatSetup<App>
    {
        protected override void InitializeIoC()
        {
            base.InitializeIoC();
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            Mvx.IoCProvider.RegisterSingleton<ISQliteAddress>(new SQliteAddress());
        }
        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            return new MvxAppCompatViewPresenter(AndroidViewAssemblies);
        }
    }
}