using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstApp.Core.ViewModels
{
    public class ChangeTaskViewModel : BaseViewModel
    {
        private int userId;
        ChangeTaskViewModel(IMvxNavigationService navigationService) : base(navigationService)
        {

        }

    }
}
