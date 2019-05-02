using FirstApp.Core.ViewModels;
using Foundation;
using MvvmCross.Platforms.Ios.Views;
using System;

namespace FirstApp.iOS.ViewControllers
{
    public partial class LoginController : MvxViewController<LoginViewModel>
    {
        public LoginController() : base (nameof(LoginController),null)
        {
        }
    }
}