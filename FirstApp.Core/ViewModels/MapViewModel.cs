using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace FirstApp.Core.ViewModels
{
   public class MapViewModel : BaseViewModel
    {
        MapViewModel(IMvxNavigationService navigationService) : base(navigationService)
        { }
    }
}
