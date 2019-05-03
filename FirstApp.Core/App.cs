using MvvmCross;
using MvvmCross.ViewModels;
using MvvmCross.IoC;
using Acr.UserDialogs;
using FirstApp.Core.Interfaces;


namespace FirstApp.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

             Mvx.IoCProvider.Resolve<IDBConnectionService>();

            Mvx.IoCProvider.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);
            // register the appstart object
            RegisterCustomAppStart<AppStart>();                  
        }
    }
}
