using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;
using Android.Views;
using Android.Widget;
using FirstApp.Core.Models;
using MvvmCross.ViewModels;

namespace FirstApp.Droid.Services
{
    //public class SimpleItemTouchHelperCallback : ItemTouchHelper.Callback
    //{
    //    // private RecyclerView.Adapter adapter;
    //    private readonly RecyclerView.Adapter _adapter;
    //    MvxObservableCollection<TaskModel> _taskList;
    //    //RecyclerViewModel lista;
    //    //NombreGenesisService lista2;
    //    public SimpleItemTouchHelperCallback(RecyclerView.Adapter adapter, MvxObservableCollection<TaskModel> taskList)
    //    {
    //        _adapter = adapter;
    //        _taskList = taskList;
    //        //lista = new RecyclerViewModel();
    //    }

    //    //public override int GetMovementFlags(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder)
    //    //{

    //    //}

    //    //public override bool OnMove(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder, RecyclerView.ViewHolder target)
    //    //{

    //    //}

    //    public override int GetMovementFlags(RecyclerView p0, RecyclerView.ViewHolder p1)
    //    {
    //        int dragFlags = ItemTouchHelper.Up | ItemTouchHelper.Down;
    //        int swipeFlags = ItemTouchHelper.Start | ItemTouchHelper.End;
    //        return MakeMovementFlags(dragFlags, swipeFlags);
    //    }

    //    public override bool OnMove(RecyclerView p0, RecyclerView.ViewHolder p1, RecyclerView.ViewHolder p2)
    //    {
    //        _adapter.NotifyItemMoved(p1.AdapterPosition, p2.AdapterPosition);
    //        return true;
    //    }

    //    public override void OnSwiped(RecyclerView.ViewHolder p0, int p1)
    //    {
    //        //lista.Items.RemoveAt(p0.AdapterPosition);
    //        _taskList.RemoveAt(p0.AdapterPosition);
    //        _adapter.NotifyItemRemoved(p0.AdapterPosition);
    //    }
    //}
}