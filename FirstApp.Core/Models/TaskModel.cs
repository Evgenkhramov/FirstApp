using FirstApp.Core.Interfaces;
using MvvmCross.Commands;
using SQLite;
using System.Collections.Generic;

namespace FirstApp.Core.Models
{
    [Table("Tasks")]
    public class TaskModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public bool Status { get; set; }
        public string FileName { get; set; }
        public string Coord { get; set; }


        [Ignore]
        public IListHandler VmHandler { get; set; }
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

        //public IMvxCommand<TaskModel> ItemClickVMCommand { get; set; }
        public IMvxCommand<TaskModel> ItemClickCommand
        {
            get
            {
                return new MvxCommand<TaskModel>((param) =>
                {
                    //ItemClickVMCommand.Execute(this);
                    VmHandler.CollectionItemClick(this);
                });              
            }

        }
    }
}
