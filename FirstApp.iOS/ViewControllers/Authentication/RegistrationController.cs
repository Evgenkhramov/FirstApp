using FirstApp.Core.ViewModels;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using System;
using UIKit;

namespace FirstApp.iOS.ViewControllers.Authentication
{
    public partial class RegistrationController : MvxViewController<RegistrationViewModel>
    {
        public RegistrationController() : base("RegistrationController", null)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();

        }

        public override void ViewDidLoad()
        {
            UserName.ShouldReturn = (textField) =>
            {
                textField.ResignFirstResponder();
                return true;
            };

            EnterUserPassword.ShouldReturn = (textField) =>
            {
                textField.ResignFirstResponder();
                return true;
            };

            EnterConfirm.ShouldReturn = (textField) =>
            {
                textField.ResignFirstResponder();
                return true;
            };

            NavigationController.NavigationBarHidden = true;
            base.ViewDidLoad();
            SetBind();
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            // hide the keyboard from all views
            View.EndEditing(true);

            base.TouchesBegan(touches, evt);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

        private void SetBind()
        {
            var set = this.CreateBindingSet<RegistrationController, RegistrationViewModel>();
            set.Bind(UserName).To(vm => vm.RegistrationUserName);
            set.Bind(EnterUserPassword).To(vm => vm.RegistrationUserPassword);
            set.Bind(EnterConfirm).To(vm => vm.RegistrationUserPasswordConfirm);
            set.Bind(RegistrationButton).To(vm => vm.UserRegistration);
            set.Bind(BackButton).To(vm => vm.BackView);

            set.Apply();
        }
    }
}