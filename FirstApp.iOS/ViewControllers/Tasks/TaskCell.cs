using FirstApp.Core.Models;
using Foundation;
using System;
using UIKit;
using FirstApp.Core;

namespace FirstApp.iOS.ViewControllers.Tasks
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
        }

        internal void UpdateCell(TaskModel task)
        {
            if (!String.IsNullOrEmpty(task.TaskName) && task.TaskName.Length > Constants.MaxLength)
            {
                TaskName.Text = task.TaskName.Substring(0, Constants.MaxLength);
            }
            if (!String.IsNullOrEmpty(task.TaskName) && task.TaskName.Length <= Constants.MaxLength)
            {
                TaskName.Text = task.TaskName;
            }
            if (!String.IsNullOrEmpty(task.TaskDescription) && task.TaskDescription.Length > Constants.MaxLength)
            {
                TaskDescription.Text = task.TaskDescription.Substring(0, Constants.MaxLength);
            }
            if (!String.IsNullOrEmpty(task.TaskDescription) && task.TaskName.Length <= Constants.MaxLength)
            {
                TaskDescription.Text = task.TaskDescription;
            }
        }
    }
}