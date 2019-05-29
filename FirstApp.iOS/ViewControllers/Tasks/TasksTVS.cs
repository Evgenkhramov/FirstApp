using FirstApp.Core;
using FirstApp.Core.Models;
using Foundation;
using MvvmCross.Platforms.Ios.Binding.Views;
using System.Windows.Input;
using UIKit;

namespace FirstApp.iOS.ViewControllers.Tasks
{
    internal class TasksTVS : MvxTableViewSource
    {
        #region Variables

        private static NSString TaskCellIdentifier;

        #endregion Variables

        #region Constructors

        public TasksTVS(UITableView tableView) : base(tableView)
        {
            TaskCellIdentifier = new NSString(Constants.TaskCell);
            tableView.RegisterNibForCellReuse(UINib.FromName(Constants.TaskCell, NSBundle.MainBundle), TaskCellIdentifier);
        }

        #endregion Constructors

        #region Commands  

        public ICommand DeleteRowCommandiOS { get; set; }

        #endregion Commands

        #region Overrides

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            var cell = (TaskCellViewController)tableView.DequeueReusableCell(Constants.TaskCell, indexPath);
            cell.UpdateCell((TaskModel)item);

            return cell;
        }

        public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
        {
            if (editingStyle == UITableViewCellEditingStyle.Delete)
            {
                DeleteRowCommandiOS.Execute(indexPath.Row);

                return;
            }

            return;
        }

        public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
        {
            return true;
        }

        public override string TitleForDeleteConfirmation(UITableView tableView, NSIndexPath indexPath)
        {
            return Constants.TrashTask;
        }

        #endregion Overrides
    }
}