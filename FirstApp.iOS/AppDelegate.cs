using FirstApp.Core;
using FirstApp.iOS;
using FirstApp.iOS.ViewControllers.Authentication;
using Foundation;
using MvvmCross.Platforms.Ios.Core;
using System;
using UIKit;

namespace Blank
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register(nameof(AppDelegate))]
    public class AppDelegate : MvxApplicationDelegate<Setup, App>
    {
        // class-level declarations

        public override UIWindow Window { get; set; }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            var result = base.FinishedLaunching(application, launchOptions);

            return result;
        }

        public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        {
            // Convert iOS NSUrl to C#/netxf/BCL System.Uri - common API
            var uriForResultView = new Uri(url.AbsoluteString);

            LoginViewController._authGoogle?.OnPageLoading(uriForResultView);

            return true;
        }
    }
}


