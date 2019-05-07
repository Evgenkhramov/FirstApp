using FirstApp.Core.Models;
using Foundation;
using System;
using UIKit;
using FirstApp.Core;

namespace FirstApp.iOS.ViewControllers
{
    public partial class TaskCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("TaskCell");
        public static readonly UINib Nib;

        static TaskCell()
        {
            Nib = UINib.FromName("TaskCell", NSBundle.MainBundle);
        }

        protected TaskCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        internal void UpdateCell(TaskModel task)
        {
            TaskName.Text = task.TaskName.Substring(0, Constants.MaxLength);
            TaskDescription.Text = task.TaskDescription.Substring(0, Constants.MaxLength);
        }
    }
}