using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;
using FirstApp.Core.Interfaces;
using FirstApp.Core.Models;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace FirstApp.Droid.Services
{

    public class SimpleItemTouchHelperCallback : ItemTouchHelper.Callback
    {
        private readonly RecyclerView.Adapter _adapter;
        MvxObservableCollection<TaskModel> _taskList;
        MvxAsyncCommand<int> _deleteCommand;
        MvxAsyncCommand<int> _deleteItemFromList;
        //private readonly IUserDialogService _userDialogService;

        public SimpleItemTouchHelperCallback(RecyclerView.Adapter adapter, MvxObservableCollection<TaskModel> taskList,
            MvxAsyncCommand<int> deleteCommand, MvxAsyncCommand<int> deleteItemFromList)
        {
            //_userDialogService = userDialogService;
            _adapter = adapter;
            _taskList = taskList;
            _deleteCommand = deleteCommand;
            _deleteItemFromList = deleteItemFromList;
        }

        public override int GetMovementFlags(RecyclerView p0, RecyclerView.ViewHolder p1)
        {
            int dragFlags = ItemTouchHelper.Up | ItemTouchHelper.Down;
            int swipeFlags = ItemTouchHelper.Start | ItemTouchHelper.End;
            return MakeMovementFlags(dragFlags, swipeFlags);
        }

        public override bool OnMove(RecyclerView p0, RecyclerView.ViewHolder p1, RecyclerView.ViewHolder p2)
        {
            _adapter.NotifyItemMoved(p1.AdapterPosition, p2.AdapterPosition);
            return false;
        }

        public override void OnSwiped(RecyclerView.ViewHolder holder, int p1)
        {
            //bool answer = _userDialogService.ShowAlertForUserWithSomeLogic("Delete this task", "Do you want to delete this task?", "Yes", "No");

            int position = holder.AdapterPosition;
            int itemId = _taskList[position].Id;
            _deleteCommand.Execute(itemId);
            _deleteItemFromList.Execute(position);
            _adapter.NotifyItemRemoved(position);
        }
    }
}