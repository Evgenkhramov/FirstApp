// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace FirstApp.iOS.ViewControllers.UserAccount
{
    [Register ("UserDataController")]
    partial class UserDataController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton CameraButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton CancelUserButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton SaveUserButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView UserImg { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField UserName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField UserSurname { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (CameraButton != null) {
                CameraButton.Dispose ();
                CameraButton = null;
            }

            if (CancelUserButton != null) {
                CancelUserButton.Dispose ();
                CancelUserButton = null;
            }

            if (SaveUserButton != null) {
                SaveUserButton.Dispose ();
                SaveUserButton = null;
            }

            if (UserImg != null) {
                UserImg.Dispose ();
                UserImg = null;
            }

            if (UserName != null) {
                UserName.Dispose ();
                UserName = null;
            }

            if (UserSurname != null) {
                UserSurname.Dispose ();
                UserSurname = null;
            }
        }
    }
}