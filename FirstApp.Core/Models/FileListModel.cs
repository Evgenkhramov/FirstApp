using FirstApp.Core.Interfaces;
using MvvmCross.Commands;
using SQLite;

namespace FirstApp.Core.Models
{
    [Table("FileName")]
    public class FileListModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string FileName { get; set; }
        
        [Ignore]
        public IFileListHandler VmHandler { get; set; }
        //public IMvxCommand<int> DeleteItemVMCommand { get; set; }
        public IMvxCommand<int> DeleteItemCommand
        {
            get
            {
                return new MvxCommand<int>((param) =>
                {
                    //DeleteItemVMCommand.Execute(Id);
                    VmHandler.RemoveCollectionItem(Id);
                });
            }
        }

    }
}
