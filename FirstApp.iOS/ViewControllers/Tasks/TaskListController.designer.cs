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
    [Register ("TaskListController")]
    partial class TaskListController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton AddNewTaskButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView TasksTable { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AddNewTaskButton != null) {
                AddNewTaskButton.Dispose ();
                AddNewTaskButton = null;
            }

            if (TasksTable != null) {
                TasksTable.Dispose ();
                TasksTable = null;
            }
        }
    }
}