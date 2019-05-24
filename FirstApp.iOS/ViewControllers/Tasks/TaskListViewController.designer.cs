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
    [Register ("TaskListViewController")]
    partial class TaskListViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView TasksTable { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (TasksTable != null) {
                TasksTable.Dispose ();
                TasksTable = null;
            }
        }
    }
}