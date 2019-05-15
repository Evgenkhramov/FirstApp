using FirstApp.Core.Models;
using FirstApp.Core.ViewModels;
using Foundation;
using MvvmCross.Platforms.Ios.Binding.Views;
using UIKit;

namespace FirstApp.iOS.ViewControllers.Tasks
{
    internal class TasksTVS : MvxTableViewSource
    {
        private static readonly NSString TaskCellIdentifier = new NSString("TaskCell");

        public TasksTVS(UITableView tableView) : base(tableView)
        {
            tableView.RegisterNibForCellReuse(UINib.FromName("TaskCell", NSBundle.MainBundle),
                                              TaskCellIdentifier);

        }

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            var cell = (TaskCell)tableView.DequeueReusableCell("TaskCell", indexPath);
            cell.UpdateCell((TaskModel)item);
            return (UITableViewCell)cell;
        }

        public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle,NSIndexPath indexPath)
        {
            switch (editingStyle)
            {
                case UITableViewCellEditingStyle.Delete:
                    // remove the item from the underlying data source
                    TaskListViewModel.RemoveCollectionItem(indexPath.Row);
                    // delete the row from the table
                    tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
                    break;
                case UITableViewCellEditingStyle.None:
                    //Console.WriteLine("CommitEditingStyle:None called");
                    break;
            }
        }
        public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
        {
            return true; // return false if you wish to disable editing for a specific indexPath or for all rows
        }
        public override string TitleForDeleteConfirmation(UITableView tableView, NSIndexPath indexPath)
        {   // Optional - default text is 'Delete'
            return "Trash Task";// + tableItems[indexPath.Row].SubHeading + ")";
        }
    }
}