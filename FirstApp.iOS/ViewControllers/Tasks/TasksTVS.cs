using FirstApp.Core.Models;
using FirstApp.iOS.ViewControllers.Tasks;
using Foundation;
using MvvmCross.Platforms.Ios.Binding.Views;
using UIKit;

namespace FirstApp.iOS.ViewControllers
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
    }
}