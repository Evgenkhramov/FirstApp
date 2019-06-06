using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using FirstApp.Core.ViewModels;
using FirstApp.Droid.Adaptere;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using System;

namespace FirstApp.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame_new, true)]
    [Register("firstApp.Droid.Views.TaskListFragment")]
    public class TaskListFragment : BaseFragment<TaskListViewModel>
    {
        #region Variables

        public MvxRecyclerView RecyclerView;
        public RecyclerView.LayoutManager LayoutManager;

        private Button _menuButton;
        protected override int FragmentId => Resource.Layout.TaskListFragment;

        #endregion Variables

        #region Methods

        public void SetupRecyclerView(View view)
        {
            RecyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.recycView);
            LayoutManager = new LinearLayoutManager(this.Context);
            RecyclerView.SetLayoutManager(LayoutManager);

            TaskListAdapter recyclerAdapter = new TaskListAdapter((IMvxAndroidBindingContext)BindingContext);
            RecyclerView.Adapter = recyclerAdapter;
        }

        #endregion Methods

        #region Overrides

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);

            SetupRecyclerView(view);

            RecyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.recycView);

            _menuButton = view.FindViewById<Button>(Resource.Id.menu_icon);

            _menuButton.Click += (object sender, EventArgs e) =>
            {
                OpenMenu();
            };

            return view;
        }

        #endregion Overrides
    }
}