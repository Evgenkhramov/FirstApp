using FirstApp.Core.ViewModels;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.Presenters;
using MvvmCross.Presenters.Attributes;
using MvvmCross.ViewModels;
using System;
using UIKit;

namespace FirstApp.iOS.ViewControllers
{
    [MvxRootPresentation]
    public partial class MainViewController : MvxTabBarViewController<MainViewModel>
    {

        private bool _firstTimePresented = true;
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
            //ViewModel.ShowMainIOS();
            //TabBar.BarTintColor = UIColor.FromRGBA(80, 90, 90, 100);
            //TabBar.TintColor = UIColor.FromRGBA(200,90,90,100);
        }

        public override void ViewWillAppear(bool animated)
        {
            if (_firstTimePresented)
            {
                _firstTimePresented = false;
                ViewModel.ShowMainFragmentCommand.Execute(null);
                ViewModel.ShowUserProfileViewModelCommand.Execute(null);
            }

            //base.ViewWillAppear(animated);
            //this.NavigationController.NavigationBarHidden = false;
            //this.NavigationController.NavigationItem.Title = "Title";
        }
    }
}