using FirstApp.Core;
using FirstApp.Core.Models;
using Foundation;
using MvvmCross.Platforms.Ios.Binding.Views;
using System.Windows.Input;
using UIKit;

namespace FirstApp.iOS.ViewControllers.Tasks
{
    internal class FileTVS : MvxTableViewSource
    {
        private static NSString FileCellIdentifier;

        public FileTVS(UITableView tableView) : base(tableView)
        {
            FileCellIdentifier = new NSString(Constants.FileItemCell);
            tableView.RegisterNibForCellReuse(UINib.FromName(Constants.FileItemCell, NSBundle.MainBundle), FileCellIdentifier);
        }

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            var cell = (FileItemCellViewController)tableView.DequeueReusableCell(Constants.FileItemCell, indexPath);
            cell.UpdateCell((FileListModel)item);

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
            return "Del File";
        }

        public ICommand DeleteRowCommandiOS { get; set; }
    }
}