using FirstApp.Core;
using FirstApp.Core.Models;
using Foundation;
using System;
using System.Windows.Input;
using UIKit;

namespace FirstApp.iOS.ViewControllers.Tasks
{
    public partial class FileItemCellViewController : UITableViewCell
    {
        public static readonly NSString Key = new NSString(Constants.FileItemCell);
        public static readonly UINib Nib;

        static FileItemCellViewController()
        {
            Nib = UINib.FromName(Constants.FileItemCell, NSBundle.MainBundle);
        }

        public FileItemCellViewController(IntPtr handle) : base(handle)
        {
        }

        internal void UpdateCell(FileListModel item)
        {
            if (!String.IsNullOrEmpty(item.FileName) && item.FileName.Length > Constants.MaxLength)
            {
                FileLabel.Text = item.FileName.Substring(0, Constants.MaxLength);
            }

            if (!String.IsNullOrEmpty(item.FileName) && item.FileName.Length <= Constants.MaxLength)
            {
                FileLabel.Text = item.FileName;
            }
        }

            public ICommand DeleteRowCommandiOS { get; set; }
    }
}
