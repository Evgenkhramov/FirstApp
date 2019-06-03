using FirstApp.Core;
using FirstApp.Core.Models;
using Foundation;
using System;
using UIKit;

namespace FirstApp.iOS.ViewControllers.Tasks
{
    public partial class TaskCellViewController : UITableViewCell
    {
        #region Variables

        public static readonly NSString Key = new NSString(Constants.TaskCell);
        public static readonly UINib Nib;

        #endregion Variables

        #region Constructors

        static TaskCellViewController()
        {
            Nib = UINib.FromName(Constants.TaskCell, NSBundle.MainBundle);
        }

        protected TaskCellViewController(IntPtr handle) : base(handle)
        {
        }

        #endregion Constructors

        #region Methods

        internal void UpdateCell(TaskRequestModel task)
        {
            if (!string.IsNullOrEmpty(task.TaskName) && task.TaskName.Length > Constants.MaxLength)
            {
                TaskName.Text = task.TaskName.Substring(0, Constants.MaxLength);
            }
            if (!string.IsNullOrEmpty(task.TaskName) && task.TaskName.Length <= Constants.MaxLength)
            {
                TaskName.Text = task.TaskName;
            }
            if (!string.IsNullOrEmpty(task.TaskDescription) && task.TaskDescription.Length > Constants.MaxLength)
            {
                TaskDescription.Text = task.TaskDescription.Substring(0, Constants.MaxLength);
            }
            if (!string.IsNullOrEmpty(task.TaskDescription) && task.TaskName.Length <= Constants.MaxLength)
            {
                TaskDescription.Text = task.TaskDescription;
            }
        }

        #endregion Methods
    }
}