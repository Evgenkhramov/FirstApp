using FirstApp.Core.ViewModels;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using System;
using UIKit;

namespace FirstApp.iOS.ViewControllers.Authentication
{
    public partial class LoginController : MvxViewController<LoginViewModel>
    {
        public LoginController() : base()
        {
        }
        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        public override void ViewDidLoad()
        {
            EnterYourLogin.ShouldReturn = (textField) => 
            {
                textField.ResignFirstResponder();
                return true;
            };

            EnterYourPasswordForLogin.ShouldReturn = (textField) => 
            {
                textField.ResignFirstResponder();
                return true;
            };
            NavigationController.NavigationBarHidden = true;
            SetBind();
            base.ViewDidLoad();
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            // hide the keyboard from all views
            View.EndEditing(true);

            base.TouchesBegan(touches, evt);
        }

        private void SetBind()
        {
            var set = this.CreateBindingSet<LoginController, LoginViewModel>();
            set.Bind(EnterYourLogin).To(vm => vm.UserName);
            set.Bind(EnterYourPasswordForLogin).To(vm => vm.UserPassword);
            set.Bind(LoginButton).To(vm => vm.UserLogin);
            set.Bind(RegistrationButton).To(vm => vm.UserRegistration);
            //set.Bind(LoginButton).For(v=> v.Enabled).To(vm => vm.UserLogin);
            //set.Bind(LoginButton).For(v=> v.Enabled).To(vm => vm.UserLogin);

            set.Apply();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }
    }
}