using FirstApp.Core.Interfaces;
using MvvmCross.Commands;

namespace FirstApp.Core.Models
{
    public class TaskRequestModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }

        public IListHandler VmHandler { get; set; }

        public IMvxCommand<int> DeleteItemCommand
        {
            get
            {
                return new MvxCommand<int>((param) =>
                {
                    VmHandler.RemoveTaskCollectionItem(Id);
                });
            }
        }

        public IMvxCommand<TaskRequestModel> ItemClickCommand
        {
            get
            {
                return new MvxCommand<TaskRequestModel>((param) =>
                {
                    VmHandler.CollectionItemClick(this);
                });
            }
        }
    }
}
