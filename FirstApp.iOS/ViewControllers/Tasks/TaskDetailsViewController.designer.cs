// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace FirstApp.iOS.ViewControllers.Tasks
{
    [Register ("TaskDetailsController")]
    partial class TaskDetailsViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton AddFileInTaskButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton AddMapMarkersButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton DeleteTaskButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView FileListView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField MapMarkersCount { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton SaveTaskButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField TaskDescription { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField TaskName { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AddFileInTaskButton != null) {
                AddFileInTaskButton.Dispose ();
                AddFileInTaskButton = null;
            }

            if (AddMapMarkersButton != null) {
                AddMapMarkersButton.Dispose ();
                AddMapMarkersButton = null;
            }

            if (DeleteTaskButton != null) {
                DeleteTaskButton.Dispose ();
                DeleteTaskButton = null;
            }

            if (FileListView != null) {
                FileListView.Dispose ();
                FileListView = null;
            }

            if (MapMarkersCount != null) {
                MapMarkersCount.Dispose ();
                MapMarkersCount = null;
            }

            if (SaveTaskButton != null) {
                SaveTaskButton.Dispose ();
                SaveTaskButton = null;
            }

            if (TaskDescription != null) {
                TaskDescription.Dispose ();
                TaskDescription = null;
            }

            if (TaskName != null) {
                TaskName.Dispose ();
                TaskName = null;
            }
        }
    }
}