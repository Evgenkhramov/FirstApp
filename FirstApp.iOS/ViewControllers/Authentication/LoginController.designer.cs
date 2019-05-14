// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace FirstApp.iOS.ViewControllers.Authentication
{
    [Register ("LoginController")]
    partial class LoginController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField EnterYourLogin { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField EnterYourPasswordForLogin { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton FacebookButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton GoogleButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton LoginButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton RegistrationButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (EnterYourLogin != null) {
                EnterYourLogin.Dispose ();
                EnterYourLogin = null;
            }

            if (EnterYourPasswordForLogin != null) {
                EnterYourPasswordForLogin.Dispose ();
                EnterYourPasswordForLogin = null;
            }

            if (FacebookButton != null) {
                FacebookButton.Dispose ();
                FacebookButton = null;
            }

            if (GoogleButton != null) {
                GoogleButton.Dispose ();
                GoogleButton = null;
            }

            if (LoginButton != null) {
                LoginButton.Dispose ();
                LoginButton = null;
            }

            if (RegistrationButton != null) {
                RegistrationButton.Dispose ();
                RegistrationButton = null;
            }
        }
    }
}