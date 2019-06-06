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

        public static readonly NSString FileKey = new NSString(Constants.FileItemCell);
        public static readonly UINib FileNib;

        #endregion Variables

        #region Constructors

        static FileItemCellViewController()
        {
            FileNib = UINib.FromName(Constants.FileItemCell, NSBundle.MainBundle);
        }

        public FileItemCellViewController(IntPtr handle) : base(handle)
        {
        }

        #endregion Constructors

        #region Methods

        internal void UpdateCell(FileRequestModel item)
        {
            if (!string.IsNullOrEmpty(item.FileName) && item.FileName.Length > Constants.MaxLength)
            {
                FileLabel.Text = item.FileName.Substring(0, Constants.MaxLength);
            }

            if (!string.IsNullOrEmpty(item.FileName) && item.FileName.Length <= Constants.MaxLength)
            {
                FileLabel.Text = item.FileName;
            }
        }

        #endregion Methods

        #region Commands 

        public ICommand DeleteRowCommandiOS { get; set; }

        #endregion Commands
    }
}
