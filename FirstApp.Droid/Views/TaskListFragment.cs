using System;
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

namespace FirstApp.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame_new, true)]
    [Register("firstApp.Droid.Views.TaskListFragment")]
    public class TaskListFragment : BaseFragment<TaskListViewModel>
    {
        public Button MenuButton;
        protected override int FragmentId => Resource.Layout.TaskListFragment;
        public MvxRecyclerView RecyclerView;
        public RecyclerView.LayoutManager LayoutManager;    

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);
            SetupRecyclerView(view);

            RecyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.recycView);

            MenuButton = view.FindViewById<Button>(Resource.Id.menu_icon);
            MenuButton.Click += (object sender, EventArgs e) =>
            {
                OpenMenu();
            };

            return view;
        }
        public void SetupRecyclerView(View view)
        {
            RecyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.recycView);
            LayoutManager = new LinearLayoutManager(this.Context);
            RecyclerView.SetLayoutManager(LayoutManager);
            TaskListAdapter recyclerAdapter = new TaskListAdapter((IMvxAndroidBindingContext)this.BindingContext);
            RecyclerView.Adapter = recyclerAdapter;
        }
    }
}