using FirstApp.Core.ViewModels;
using Foundation;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using System;
using UIKit;

namespace FirstApp.iOS.ViewControllers.Authentication
{
    public partial class StartController : MvxViewController<StartViewModel>
    {

        public StartController() : base("StartController", null)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        public override void ViewDidLoad()
        {
            
            NavigationController.NavigationBarHidden = true;
            base.ViewDidLoad();
            ViewModel.ShowLoginFragmentCommand.Execute(null);
        }

        public override void ViewWillAppear(bool animated)
         {
            base.ViewWillAppear(animated);
            //this.NavigationController.NavigationBarHidden = false;
            //this.NavigationController.NavigationItem.Title = "Title";
        }

    }
}