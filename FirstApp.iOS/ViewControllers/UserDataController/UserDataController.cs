using Foundation;
using MvvmCross.Platforms.Ios.Views;
using FirstApp.Core.ViewModels;
using UIKit;

namespace FirstApp.iOS.ViewControllers
{
    public class UserDataController : MvxViewController<UserDatatViewModel>
    {
        public UserDataController() : base(nameof(UserDataController), null)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            //this.NavigationController.NavigationBarHidden = false;
            //this.NavigationController.NavigationItem.Title = "Title";
        }
    }
}