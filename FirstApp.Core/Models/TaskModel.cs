using FirstApp.Core.Interfaces;
using MvvmCross.Commands;
using SQLite;
namespace FirstApp.Core.Models
{
    [Table("Tasks")]
    public class TaskModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public bool Status { get; set; }
        public string FileName { get; set; }
        public string Coord { get; set; }

        [Ignore]
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

        public IMvxCommand<TaskModel> ItemClickCommand
        {
            get
            {
                return new MvxCommand<TaskModel>((param) =>
                {
                    VmHandler.CollectionItemClick(this);
                });
            }
        }
    }
}
