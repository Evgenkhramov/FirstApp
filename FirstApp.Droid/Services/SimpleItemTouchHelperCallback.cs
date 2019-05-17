using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;
using Android.Views;
using Android.Widget;
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

        public SimpleItemTouchHelperCallback(RecyclerView.Adapter adapter, MvxObservableCollection<TaskModel> taskList,
            MvxAsyncCommand<int> deleteCommand, MvxAsyncCommand<int> deleteItemFromList)
        {       
            _adapter = adapter;
            _taskList = taskList;
            _deleteCommand = deleteCommand;
            _deleteItemFromList = deleteItemFromList;
        }

        public override void OnChildDraw(Canvas c, RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder, float dX, float dY, int actionState, bool isCurrentlyActive)
        {
            base.OnChildDraw(c, recyclerView, viewHolder, dX, dY, actionState, isCurrentlyActive);
            
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
        }
    }
}