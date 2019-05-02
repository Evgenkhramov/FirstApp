using FirstApp.Core.ViewModels;
using Foundation;
using MvvmCross.Platforms.Ios.Views;
using System;

namespace FirstApp.iOS
{
    public partial class UserDataController : MvxViewController<UserDataViewModel>
    {
        public UserDataController() : base(nameof(UserDataController), null)
        {
        }
    }
}