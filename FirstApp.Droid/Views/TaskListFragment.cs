using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Gms.Maps.Model;
using Android.Gms.Maps;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FirstApp.Core.ViewModels;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Android.Support.V7.Widget;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using FirstApp.Droid.Adaptere;
using MvvmCross.ViewModels;
using FirstApp.Core.Models;
using Android.Support.V7.Widget.Helper;
using FirstApp.Droid.Services;

namespace FirstApp.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame_new, true)]
    [Register("firstApp.Droid.Views.TaskListFragment")]
    public class TaskListFragment : BaseFragment<TaskListViewModel>
    {
        public Button menuButton;
        protected override int FragmentId => Resource.Layout.TaskListFragment;
        MvxRecyclerView _recyclerView;
        RecyclerView.LayoutManager _layoutManager;
        //public MvxObservableCollection<TaskModel> TaskCollection;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            SetupRecyclerView(view);

            _recyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.recycView);

            menuButton = view.FindViewById<Button>(Resource.Id.menu_icon);
            menuButton.Click += (object sender, EventArgs e) =>
            {
                OpenMenu();
            };

            return view;
        }
        public void SetupRecyclerView(View view)
        {
            // TaskCollection = new MvxObservableCollection<TaskModel>();
            _recyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.recycView);
            _layoutManager = new LinearLayoutManager(this.Context);
            _recyclerView.SetLayoutManager(_layoutManager);
            var recyclerAdapter = new TaskListAdapter((IMvxAndroidBindingContext)this.BindingContext);

            _recyclerView.Adapter = recyclerAdapter;

            ItemTouchHelper.Callback callback = new SimpleItemTouchHelperCallback(recyclerAdapter, ViewModel.TaskCollection);
            ItemTouchHelper itemTouchHelper = new ItemTouchHelper(callback);
            itemTouchHelper.AttachToRecyclerView(_recyclerView);

        }
        public void DeleteTask(int taskId)
        {
            ViewModel.DeleteItem.Execute();
        }
        public class SimpleItemTouchHelperCallback : ItemTouchHelper.Callback
        {

            // private RecyclerView.Adapter adapter;
            private readonly RecyclerView.Adapter _adapter;
            MvxObservableCollection<TaskModel> _taskList;
            //RecyclerViewModel lista;
            //NombreGenesisService lista2;
            public SimpleItemTouchHelperCallback(RecyclerView.Adapter adapter, MvxObservableCollection<TaskModel> taskList)
            {
                _adapter = adapter;
                _taskList = taskList;
                //lista = new RecyclerViewModel();
            }

            //public override int GetMovementFlags(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder)
            //{

            //}

            //public override bool OnMove(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder, RecyclerView.ViewHolder target)
            //{

            //}

            public override int GetMovementFlags(RecyclerView p0, RecyclerView.ViewHolder p1)
            {
                int dragFlags = ItemTouchHelper.Up | ItemTouchHelper.Down;
                int swipeFlags = ItemTouchHelper.Start | ItemTouchHelper.End;
                return MakeMovementFlags(dragFlags, swipeFlags);
            }

            public override bool OnMove(RecyclerView p0, RecyclerView.ViewHolder p1, RecyclerView.ViewHolder p2)
            {
                _adapter.NotifyItemMoved(p1.AdapterPosition, p2.AdapterPosition);
                return true;
            }

            public override void OnSwiped(RecyclerView.ViewHolder p0, int p1)
            {
                DeleteTask
                ViewModel.taskItem = p0.AdapterPosition;
                //lista.Items.RemoveAt(p0.AdapterPosition);
                ViewModel.DeleteItem.Execute();
                _taskList.RemoveAt(p0.AdapterPosition);
                _adapter.NotifyItemRemoved(p0.AdapterPosition);
            }
        }
    }
}