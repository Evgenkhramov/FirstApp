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
        #region Variables

        public static readonly NSString FileKey;
        public static readonly UINib FileNib;

        #endregion Variables

        #region Constructors

        static FileItemCellViewController()
        {
            FileKey = new NSString(Constants.FileItemCell);
            FileNib = UINib.FromName(Constants.FileItemCell, NSBundle.MainBundle);
        }

        public FileItemCellViewController(IntPtr handle) : base(handle)
        {
        }

        #endregion Constructors

        #region Methods

        internal void UpdateCell(FileRequestModel item)
        {
            if (!string.IsNullOrEmpty(item.FileName))
            {
                return;
            }

            if (item.FileName.Length > Constants.MaxLength)
            {
                FileLabel.Text = item.FileName.Substring(default(int), Constants.MaxLength);
            }

            FileLabel.Text = item.FileName;
        }

        #endregion Methods

        #region Commands 

        public ICommand DeleteRowCommandiOS { get; set; }

        #endregion Commands
    }
}
