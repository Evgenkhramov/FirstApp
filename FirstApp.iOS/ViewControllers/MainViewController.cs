using FirstApp.Core.ViewModels;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

namespace FirstApp.iOS.ViewControllers
{
    [MvxRootPresentation]
    public partial class MainViewController : MvxViewController<MainViewModel>
    {
        //public MainViewController() : base("MainViewController", null)
        //{
        //}

        public MainViewController()
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
        }
    }
}