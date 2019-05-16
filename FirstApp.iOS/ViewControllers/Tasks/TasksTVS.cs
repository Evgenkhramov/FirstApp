using FirstApp.Core.Models;
using Foundation;
using MvvmCross.Platforms.Ios.Binding.Views;
using System.Windows.Input;
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

        public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
        {
            switch (editingStyle)
            {
                case UITableViewCellEditingStyle.Delete:
                    DeleteRowCommandiOS.Execute(indexPath.Row);
                    // remove the item from the underlying data source
                    //if (ItemsSource is MvxObservableCollection<TaskModel> sourceCollection)
                    //{
                    //    sourceCollection.RemoveAt(indexPath.Row);
                    //}
                    break;
                case UITableViewCellEditingStyle.None:

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

        public ICommand DeleteRowCommandiOS { get; set; }
    }
}