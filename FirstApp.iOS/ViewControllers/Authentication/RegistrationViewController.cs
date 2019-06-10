using CoreGraphics;
using FirstApp.Core;
using FirstApp.Core.ViewModels;
using FirstApp.iOS.Helpers;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using System;
using UIKit;

namespace FirstApp.iOS.ViewControllers.Authentication
{
    public partial class RegistrationViewController : MvxViewController<RegistrationViewModel>
    {
        #region Variables

        private UIView _activeview;
        private nfloat _scrollAmount;
        private bool _isMoveViewUp;

        #endregion Variables

        #region Constructors

        public RegistrationViewController() : base(nameof(RegistrationViewController), null)
        {
            _scrollAmount = default(float);
            _isMoveViewUp = false;
        }

        #endregion Constructors

        #region Methods

        private void SetBind()
        {
            MvxFluentBindingDescriptionSet<RegistrationViewController, RegistrationViewModel> set = this.CreateBindingSet<RegistrationViewController, RegistrationViewModel>();

            set.Bind(UserName).To(vm => vm.RegistrationUserName);
            set.Bind(UserEmail).To(vm => vm.RegistrationEmail);
            set.Bind(EnterUserPassword).To(vm => vm.RegistrationUserPassword);
            set.Bind(EnterConfirm).To(vm => vm.RegistrationUserPasswordConfirm);
            set.Bind(RegistrationButton).To(vm => vm.UserRegistrationCommand);
            set.Bind(BackButton).To(vm => vm.BackViewCommand);

            set.Apply();
        }

        private void KeyBoardUpNotification(NSNotification notification)
        {
            _activeview = ScrollViewTopHelper.GetActiveView(View);

            CGRect keyBourdSize = UIKeyboard.BoundsFromNotification(notification);

            _scrollAmount = ScrollViewTopHelper.GetScrollAmount(_activeview, keyBourdSize);

            if (_scrollAmount <= default(float))
            {
                _isMoveViewUp = false;
                return;
            }

            _isMoveViewUp = true;

            ScrollTheView(_isMoveViewUp);
        }

        private void KeyBoardDownNotification(NSNotification notification)
        {
            cnsTopConstrain.Constant = default(int);
            MainScrollView.UpdateConstraints();
        }

        private void ScrollTheView(bool move)
        {
            cnsTopConstrain.Constant = -_scrollAmount;
            MainScrollView.UpdateConstraints();
        }

        #endregion Methods

        #region Overrides

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = Constants.UserRegistration;

            EdgesForExtendedLayout = UIRectEdge.None;

            UIView view = View;

            NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.DidShowNotification, KeyBoardUpNotification);

            NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, KeyBoardDownNotification);

            UserName.ShouldReturn = (textField) =>
            {
                textField.ResignFirstResponder();

                return true;
            };

            UserEmail.ShouldReturn = (textField) =>
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

            SetBind();
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            View.EndEditing(true);

            base.TouchesBegan(touches, evt);
        }

        #endregion Overrides
    }
}