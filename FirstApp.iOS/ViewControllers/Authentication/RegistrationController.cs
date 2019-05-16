using FirstApp.Core.ViewModels;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using System;
using UIKit;
using FirstApp.iOS.Helpers;
using CoreGraphics;

namespace FirstApp.iOS.ViewControllers.Authentication
{
    public partial class RegistrationController : MvxViewController<RegistrationViewModel>
    {
        public UIView activeview;             // Controller that activated the keyboard
        public nfloat scrollAmount = 0.0f;    // amount to scroll                  
        private bool moveViewUp = false;

        public RegistrationController() : base("RegistrationController", null)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();

        }

        public override void ViewDidLoad()
        {
            Title = "User Registration";
            EdgesForExtendedLayout = UIRectEdge.None;

            UIView view = this.View;

            NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.DidShowNotification, KeyBoardUpNotification);
            // Keyboard Down
            NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, KeyBoardDownNotification);


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

        private void KeyBoardUpNotification(NSNotification notification)
        {
            activeview = ScrollViewTopHelper.GetActiveView(this.View);
            CGRect keyBourdSize = UIKeyboard.BoundsFromNotification(notification);
            scrollAmount = ScrollViewTopHelper.GetScrollAmount(activeview, keyBourdSize);
            // Perform the scrolling
            if (scrollAmount > 0)
            {
                moveViewUp = true;
                ScrollTheView(moveViewUp);
            }
            else
            {
                moveViewUp = false;
            }
        }
        private void KeyBoardDownNotification(NSNotification notification)
        {
            cnsTopConstrain.Constant = 0;
            MainScrollView.UpdateConstraints();
        }

        private void ScrollTheView(bool move)
        {
            cnsTopConstrain.Constant = -scrollAmount;
            MainScrollView.UpdateConstraints();
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
            set.Bind(RegistrationButton).To(vm => vm.UserRegistrationCommand);
            set.Bind(BackButton).To(vm => vm.BackViewCommand);

            set.Apply();
        }
    }
}