using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters;
using FirstApp.Core;
using MvvmCross.IoC;

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
        }
        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            return new MvxAppCompatViewPresenter(AndroidViewAssemblies);
        }
    }
}