using FirstApp.Core.ViewModels;
using Foundation;
using MvvmCross.Platforms.Ios.Views;
using System;

namespace FirstApp.iOS.ViewControllers
{
    public partial class RegistrationController : MvxViewController<RegistrationViewModel>
    {
        public RegistrationController () : base (nameof(RegistrationController), null)
        {
        }
    }
}