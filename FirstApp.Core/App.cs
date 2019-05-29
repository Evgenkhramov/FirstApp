﻿using FirstApp.Core.Interfaces;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;

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

            RegisterCustomAppStart<AppStart>();                  
        }
    }
}
