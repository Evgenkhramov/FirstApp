using Acr.UserDialogs;
using AVFoundation;
using CoreGraphics;
using FirstApp.Core.ViewModels;
using FirstApp.iOS.Converters;
using Foundation;
using MvvmCross;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.Plugin.PictureChooser;
using MvvmCross.Presenters;
using MvvmCross.Presenters.Attributes;
using MvvmCross.ViewModels;
using Photos;
using System;
using System.IO;
using System.Threading.Tasks;
using UIKit;

namespace FirstApp.iOS.ViewControllers.UserAccount
{
    [MvxTabPresentation(WrapInNavigationController = true, TabName = "User Profile", TabIconName = "userProfile")]

    public partial class UserDataController : MvxViewController<UserDataViewModel>
    {

        public UIView activeview;             // Controller that activated the keyboard
        public nfloat scroll_amount = 0.0f;    // amount to scroll 
        public nfloat bottom = 0.0f;           // bottom point
        public nfloat offset = 10.0f;          // extra offset
        private bool moveViewUp = false;

        public UserDataController()
        {
           
        }

        public MvxBasePresentationAttribute PresentationAttribute()
        {
            return new MvxModalPresentationAttribute
            {
                ModalPresentationStyle = UIModalPresentationStyle.PageSheet,
                ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve
            };
        }


        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        public override void ViewDidLoad()
        {
            Title = "User Profile";

            EdgesForExtendedLayout = UIRectEdge.None;

            // Keyboard popup
            NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.DidShowNotification, KeyBoardUpNotification);
            // Keyboard Down
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

            //NavigationController.NavigationBarHidden = true;

            var set = this.CreateBindingSet<UserDataController, UserDataViewModel>();
            set.Bind(UserName).To(vm => vm.UserName);
            set.Bind(UserSurname).To(vm => vm.Surname);
            set.Bind(UserImg).For(v => v.Image).To(vm => vm.MyPhoto).WithConversion("ByteArrayToImg").TwoWay();
            set.Bind(SaveUserButton).To(vm => vm.SaveUserDataiOS);
            set.Bind(CancelUserButton).To(vm => vm.Cancel);

            set.Apply();

            base.ViewDidLoad();
        }

        public void GetActiveView(UIView view)
        {
            foreach (UIView item in view.Subviews)
            {
                if (view.IsFirstResponder)
                {
                    activeview = item;
                    return;
                }
                else
                    GetActiveView(item);
            }
        }

        private void KeyBoardUpNotification(NSNotification notification)
        {


            // get the keyboard size
            CGRect r = UIKeyboard.BoundsFromNotification(notification);
            
            cnsBottomScroll.Constant = r.Height;
            MainScroll.UpdateConstraints();
            //GetActiveView(this.View);
            // Find what opened the keyboard
            //foreach (UIView view in this.View.Subviews)
            //{
            //    if (view.IsFirstResponder)
            //        activeview = view;
            //}

            // Bottom of the controller = initial position + height + offset      
           // bottom = (activeview.Frame.Y + activeview.Frame.Height + offset);

            // Calculate how far we need to scroll
           // scroll_amount = (r.Height - (View.Frame.Size.Height - bottom));

            // Perform the scrolling
            //if (scroll_amount > 0)
            //{
            //    moveViewUp = true;
            //    ScrollTheView(moveViewUp);
            //}
            //else
            //{
            //    moveViewUp = false;
            //}
        }
        private void KeyBoardDownNotification(NSNotification notification)
        {
            cnsBottomScroll.Constant = 0;
            MainScroll.UpdateConstraints();
        }

        //private void ScrollTheView(bool move)
        //{

        //    // scroll the view up or down
        //    UIView.BeginAnimations(string.Empty, System.IntPtr.Zero);
        //    UIView.SetAnimationDuration(0.3);

        //    CGRect frame = View.Frame;

        //    if (move)
        //    {
        //        frame.Y -= scroll_amount;
        //    }
        //    else
        //    {
        //        frame.Y += scroll_amount;
        //        scroll_amount = 0;
        //    }

        //    View.Frame = frame;
        //    UIView.CommitAnimations();
        //}

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            // hide the keyboard from all views
            View.EndEditing(true);

            base.TouchesBegan(touches, evt);
        }

        public override void ViewWillAppear(bool animated)
        {

            //base.ViewWillAppear(animated);
            //this.NavigationController.NavigationBarHidden = false;
            //this.NavigationController.NavigationItem.Title = "Title";
        }

        public async Task SelectPhoto()
        {
            bool answ = await Mvx.IoCProvider.Resolve<IUserDialogs>().ConfirmAsync("Please, select photo.", "Select Photo", "From memory", "From camera");

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