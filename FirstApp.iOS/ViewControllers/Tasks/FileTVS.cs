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
        #region Variables

        private static NSString FileCellIdentifier;

        #endregion Variables

        #region Constructors

        public FileTVS(UITableView tableView) : base(tableView)
        {
            FileCellIdentifier = new NSString(Constants.FileItemCell);
            tableView.RegisterNibForCellReuse(UINib.FromName(Constants.FileItemCell, NSBundle.MainBundle), FileCellIdentifier);
        }

        #endregion Constructors

        #region Overrides

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            FileItemCellViewController cell = (FileItemCellViewController)tableView.DequeueReusableCell(Constants.FileItemCell, indexPath);
            cell.UpdateCell((FileRequestModel)item);

            return cell;
        }

        public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
        {
            if (editingStyle == UITableViewCellEditingStyle.Delete
                && ItemsSource is System.Collections.ObjectModel.ObservableCollection<FileRequestModel> sourceCollection)
            {
                int fileId = sourceCollection[indexPath.Row].Id;
                DeleteRowCommandiOS.Execute(fileId);

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
            return Constants.DeleteFile;
        }

        #endregion Overrides

        #region Commands  

        public ICommand DeleteRowCommandiOS { get; set; }

        #endregion Commands
    }
}