using FirstApp.Core;
using FirstApp.Core.Interfaces;
using FirstApp.iOS.Services;
using MvvmCross;
using MvvmCross.Base;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.IoC;
using MvvmCross.Platforms.Ios.Core;
using MvvmCross.Plugin.Json;
using MvvmCross.ViewModels;
using UIKit;

namespace FirstApp.iOS
{
    public class Setup : MvxIosSetup<App>
    {
        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();

            Mvx.RegisterType<IMvxJsonConverter, MvxJsonConverter>();
        }

        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();

            var registry = Mvx.Resolve<IMvxTargetBindingFactoryRegistry>();
        }
        protected override IMvxApplication CreateApp()
        {
            CreatableTypes()
                      .EndingWith("Service")
                      .AsInterfaces()
                      .RegisterAsLazySingleton();
            Mvx.RegisterType<IDBConnectionService, DBConnectionService>();
           // Mvx.RegisterType<IPhotoService, PhotoService>();
            return new Core.App();
        }

        //protected override IMvxIocOptions CreateIocOptions()
        //{
        //    return new MvxIocOptions
        //    {
        //        PropertyInjectorOptions = MvxPropertyInjectorOptions.MvxInject
        //    };
        //}
    }
}
