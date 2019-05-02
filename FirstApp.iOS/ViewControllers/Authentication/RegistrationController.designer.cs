// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace FirstApp.iOS
{
    [Register ("RegistrationController")]
    partial class RegistrationController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField EnterPasswordСonfirm { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField EnterUserPassword { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton RegistrationButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (EnterPasswordСonfirm != null) {
                EnterPasswordСonfirm.Dispose ();
                EnterPasswordСonfirm = null;
            }

            if (EnterUserPassword != null) {
                EnterUserPassword.Dispose ();
                EnterUserPassword = null;
            }

            if (RegistrationButton != null) {
                RegistrationButton.Dispose ();
                RegistrationButton = null;
            }
        }
    }
}