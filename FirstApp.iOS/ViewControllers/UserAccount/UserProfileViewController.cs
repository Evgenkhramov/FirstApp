using Acr.UserDialogs;
using AVFoundation;
using CoreGraphics;
using FirstApp.Core;
using FirstApp.Core.ViewModels;
using FirstApp.iOS.Helpers;
using Foundation;
using MvvmCross;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using Photos;
using System;
using System.Threading.Tasks;
using UIKit;

namespace FirstApp.iOS.ViewControllers.UserAccount
{
    [MvxTabPresentation(WrapInNavigationController = true, TabName = "User Profile", TabIconName = "userProfile")]

    public partial class UserProfileViewController : MvxViewController<UserDataViewModel>
    {
        #region Variables

        private UIView _activeview;
        private nfloat _scrollAmount;
        private bool _isViewMoveUp;
        private IUserDialogs _userDialogs;

        #endregion Variables

        #region Constructors

        public UserProfileViewController()
        {
            _scrollAmount = default(nfloat);
            _isViewMoveUp = false;
            _userDialogs = Mvx.IoCProvider.Resolve<IUserDialogs>();
        }

        #endregion Constructors

        #region Methods

        private void SetBind()
        {
            var set = this.CreateBindingSet<UserProfileViewController, UserDataViewModel>();

            set.Bind(UserName).To(vm => vm.UserName);
            set.Bind(UserSurname).To(vm => vm.Surname);
            set.Bind(UserImg).For(v => v.Image).To(vm => vm.MyPhoto).WithConversion(Constants.ByteArrayToImg).TwoWay();
            set.Bind(SaveUserButton).To(vm => vm.SaveUserData);
            set.Bind(CancelUserButton).To(vm => vm.Cancel);
            set.Bind(LogOutButton).To(vm => vm.LogOutCommand);

            set.Apply();
        }

        private void KeyBoardUpNotification(NSNotification notification)
        {
            _activeview = ScrollViewTopHelper.GetActiveView(View);

            CGRect keyBourdSize = UIKeyboard.BoundsFromNotification(notification);

            _scrollAmount = ScrollViewTopHelper.GetScrollAmount(_activeview, keyBourdSize);

            if (_scrollAmount <= default(int))
            {
                _isViewMoveUp = false;
                return;
            }

            _isViewMoveUp = true;

            ScrollTheView(_isViewMoveUp);
        }

        private void KeyBoardDownNotification(NSNotification notification)
        {
            cnsButtomScroll.Constant = 0;

            MainScroll.UpdateConstraints();
        }

        private void ScrollTheView(bool move)
        {
            cnsButtomScroll.Constant = -_scrollAmount;

            MainScroll.UpdateConstraints();
        }

        public async Task SelectPhoto()
        {
            bool isUserAccept = await _userDialogs.ConfirmAsync(Constants.PleaseSelectPhoto, Constants.SelectPhoto, Constants.FromMemory, Constants.FromCamera);

            if (isUserAccept)
            {
                GetFromMemory();
                return;
            }

            GetFromCamera();
        }

        private void GetFromMemory()
        {
            PHAuthorizationStatus photos = PHPhotoLibrary.AuthorizationStatus;
            if (photos == PHAuthorizationStatus.Authorized)
            {
                GetPhoto();
            }
            if (photos != PHAuthorizationStatus.Authorized)
            {
                PHPhotoLibrary.RequestAuthorization(status =>
                {
                    if (status == PHAuthorizationStatus.Authorized)
                    {
                        return;
                    }

                });
            }
        }

        private void GetFromCamera()
        {
            AVAuthorizationStatus authStatus = AVCaptureDevice.GetAuthorizationStatus(AVMediaType.Video);

            if (authStatus == AVAuthorizationStatus.Authorized)
            {
                DoPhoto();
                return;
            }

            if (authStatus != AVAuthorizationStatus.Authorized)
            {
                AVCaptureDevice.RequestAccessForMediaType(AVAuthorizationMediaType.Video, (bool access) =>
                {
                    if (access == true)
                    {
                        DoPhoto();
                    };
                });
                return;
            }
        }

        public void GetPhoto()
        {
            ViewModel.ChoosePictureCommand.Execute(null);
        }

        public void DoPhoto()
        {
            ViewModel.TakePictureCommand.Execute(null);
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

            SetBind();

            Title = Constants.UserProfile;

            EdgesForExtendedLayout = UIRectEdge.None;

            UIView view = View;

            NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.DidShowNotification, KeyBoardUpNotification);

            NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, KeyBoardDownNotification);

            UserName.ShouldReturn = (textField) =>
            {
                textField.ResignFirstResponder();
                return true;
            };

            UserSurname.ShouldReturn = (textField) =>
            {
                textField.ResignFirstResponder();
                return true;
            };

            CameraButton.TouchUpInside += (sender, e) =>
            {
                SelectPhoto();
            };       
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            View.EndEditing(true);

            base.TouchesBegan(touches, evt);
        }

        #endregion Overrides
    }
}