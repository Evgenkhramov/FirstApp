using FirstApp.Core.Interfaces;
using MvvmCross.Commands;

namespace FirstApp.Core.Models
{
    public class FileRequestModel
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string FileName { get; set; }

        public IFileListHandler VmHandler { get; set; }
        public IMvxCommand<int> DeleteItemCommand
        {
            get
            {
                return new MvxCommand<int>((param) =>
                {
                    VmHandler.RemoveFileCollectionItem(Id);
                });
            }
        }
    }
}
