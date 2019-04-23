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
using MvvmCross.Commands;
using FirstApp.Core.Interfaces;

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
        private readonly IUserDialogService _userDialogService;
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
            _recyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.recycView);
            _layoutManager = new LinearLayoutManager(this.Context);
            _recyclerView.SetLayoutManager(_layoutManager);
            var recyclerAdapter = new TaskListAdapter((IMvxAndroidBindingContext)this.BindingContext);
            _recyclerView.Adapter = recyclerAdapter;

            //ItemTouchHelper.Callback callback = new SimpleItemTouchHelperCallback(recyclerAdapter, ViewModel.TaskCollection, ViewModel.DeleteItem, ViewModel.DeleteItemFromList);
            //ItemTouchHelper itemTouchHelper = new ItemTouchHelper(callback);
            //itemTouchHelper.AttachToRecyclerView(_recyclerView);

        }
    }
}