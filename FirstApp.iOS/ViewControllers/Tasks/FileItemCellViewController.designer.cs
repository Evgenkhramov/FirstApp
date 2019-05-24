// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace FirstApp.iOS.ViewControllers.Tasks
{
    [Register ("FileItemCellViewController")]
    partial class FileItemCellViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel FileLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (FileLabel != null) {
                FileLabel.Dispose ();
                FileLabel = null;
            }
        }
    }
}