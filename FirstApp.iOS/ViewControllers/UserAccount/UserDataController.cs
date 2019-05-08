using Acr.UserDialogs;
using AVFoundation;
using FirstApp.Core.ViewModels;
using MvvmCross;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.Plugin.PictureChooser;
using Photos;
using System.IO;
using System.Threading.Tasks;

namespace FirstApp.iOS.ViewControllers.UserAccount
{
    [MvxTabPresentation(WrapInNavigationController = true, TabName = "User Profile", TabIconName = "userProfile")]
    public partial class UserDataController : MvxViewController<UserDataViewModel>
    {
        public UserDataController()
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

            UserSurname.ShouldReturn = (textField) =>
            {
                textField.ResignFirstResponder();
                return true;
            };

            CameraButton.TouchUpInside += (sender, e) =>
            {
                ChoosePhoto();
            };

            //NavigationController.NavigationBarHidden = true;

            var set = this.CreateBindingSet<UserDataController, UserDataViewModel>();
            set.Bind(UserName).To(vm => vm.UserName);
            set.Bind(UserSurname).To(vm => vm.Surname);
            set.Bind(UserImg).For(v => v.Image).To(vm => vm.MyPhoto).WithConversion("InMemoryImage");
            set.Bind(SaveUserButton).To(vm => vm.SaveUserData);
            set.Bind(CancelUserButton).To(vm => vm.Cancel);

            set.Apply();

            base.ViewDidLoad();
        }

        public override void ViewWillAppear(bool animated)
        {

            //base.ViewWillAppear(animated);
            //this.NavigationController.NavigationBarHidden = false;
            //this.NavigationController.NavigationItem.Title = "Title";
        }

        public void ChoosePhoto()
        {

            AVAuthorizationStatus authStatus = AVCaptureDevice.GetAuthorizationStatus(AVMediaType.Video);
            if (authStatus == AVAuthorizationStatus.Authorized)
            {
                SelectPhoto();
                return;
            }

            if (authStatus != AVAuthorizationStatus.Authorized)
            {
                AVCaptureDevice.RequestAccessForMediaType(AVAuthorizationMediaType.Video, (bool access) =>
                {
                    if (access == true)
                    {
                        SelectPhoto();
                    };
                });
                return;
            }

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