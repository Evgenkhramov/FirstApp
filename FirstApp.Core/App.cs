using MvvmCross;
using MvvmCross.ViewModels;
using FirstApp.Core.Services;
using FirstApp.Core.ViewModels;
using MvvmCross.Navigation;
using System.Threading.Tasks;
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

            // register the appstart object
            RegisterCustomAppStart<AppStart>();
            Mvx.IoCProvider.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);          
        }
    }
}
