using Acr.UserDialogs;
using AVFoundation;
using CoreGraphics;
using FirstApp.Core.ViewModels;
using Foundation;
using MvvmCross;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using Photos;
using System;
using FirstApp.iOS.Helpers;
using System.Threading.Tasks;
using UIKit;
using FirstApp.Core;

namespace FirstApp.iOS.ViewControllers.UserAccount
{
    [MvxTabPresentation(WrapInNavigationController = true, TabName = "User Profile", TabIconName = "userProfile")]

    public partial class UserDataController : MvxViewController<UserDataViewModel>
    {
        public UIView activeview;             
        public nfloat scrollAmount = 0.0f;                    
        private bool _moveViewUp = false;

        public UserDataController()
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "User Profile";

            EdgesForExtendedLayout = UIRectEdge.None;

            UIView view = this.View;

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

            var set = this.CreateBindingSet<UserDataController, UserDataViewModel>();
            set.Bind(UserName).To(vm => vm.UserName);
            set.Bind(UserSurname).To(vm => vm.Surname);
            set.Bind(UserImg).For(v => v.Image).To(vm => vm.MyPhoto).WithConversion("ByteArrayToImg").TwoWay();
            set.Bind(SaveUserButton).To(vm => vm.SaveUserData);
            set.Bind(CancelUserButton).To(vm => vm.Cancel);

            set.Apply();     
        }   

        private void KeyBoardUpNotification(NSNotification notification)
        {
            activeview = ScrollViewTopHelper.GetActiveView(this.View);
            CGRect keyBourdSize = UIKeyboard.BoundsFromNotification(notification);
            scrollAmount = ScrollViewTopHelper.GetScrollAmount(activeview, keyBourdSize);      
            
            if (scrollAmount > 0)
            {
                _moveViewUp = true;
                ScrollTheView(_moveViewUp);
            }
            if (scrollAmount <= 0)
            {
                _moveViewUp = false;
            }
        }

        private void KeyBoardDownNotification(NSNotification notification)
        {
            cnsButtomScroll.Constant = 0;
            MainScroll.UpdateConstraints();
        }

        private void ScrollTheView(bool move)
        {
            cnsButtomScroll.Constant = -scrollAmount;
            MainScroll.UpdateConstraints();
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            View.EndEditing(true);

            base.TouchesBegan(touches, evt);
        }

        public override void ViewWillAppear(bool animated)
        {       
        }

        public async Task SelectPhoto()
        {
            bool answ = await Mvx.IoCProvider.Resolve<IUserDialogs>().ConfirmAsync(Constants.PleaseSelectPhoto, Constants.SelectPhoto, Constants.FromMemory, Constants.FromCamera);

            if (answ)
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
                        switch (status)
                        {
                            case PHAuthorizationStatus.Authorized:
                                GetPhoto();
                                break;
                            case PHAuthorizationStatus.Denied:
                                break;
                            case PHAuthorizationStatus.Restricted:
                                break;
                            default:
                                break;
                        }
                    });
                }
            }
            if (!answ)
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
        }

        public async Task GetPhoto()
        {
            ViewModel.ChoosePictureCommand.Execute(null);
        }

        public async Task DoPhoto()
        {
            ViewModel.TakePictureCommand.Execute(null);
        }
    }
}