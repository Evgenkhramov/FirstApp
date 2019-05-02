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
    [Register ("ListItemController")]
    partial class ListItemController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton DeleteTaskButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField TaskDescriptionText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField TaskNameText { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (DeleteTaskButton != null) {
                DeleteTaskButton.Dispose ();
                DeleteTaskButton = null;
            }

            if (TaskDescriptionText != null) {
                TaskDescriptionText.Dispose ();
                TaskDescriptionText = null;
            }

            if (TaskNameText != null) {
                TaskNameText.Dispose ();
                TaskNameText = null;
            }
        }
    }
}