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
        private static NSString TaskCellIdentifier;

        public TasksTVS(UITableView tableView) : base(tableView)
        {
            TaskCellIdentifier = new NSString(Constants.TaskCell);
            tableView.RegisterNibForCellReuse(UINib.FromName(Constants.TaskCell, NSBundle.MainBundle), TaskCellIdentifier);
        }

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            var cell = (TaskCellViewController)tableView.DequeueReusableCell(Constants.TaskCell, indexPath);
            cell.UpdateCell((TaskModel)item);

            return (UITableViewCell)cell;
        }

        public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
        {
            switch (editingStyle)
            {
                case UITableViewCellEditingStyle.Delete:
                    DeleteRowCommandiOS.Execute(indexPath.Row);

                    break;
                case UITableViewCellEditingStyle.None:

                    break;
            }
        }

        public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
        {
            return true;
        }

        public override string TitleForDeleteConfirmation(UITableView tableView, NSIndexPath indexPath)
        {   
            return "Trash Task";
        }

        public ICommand DeleteRowCommandiOS { get; set; }
    }
}