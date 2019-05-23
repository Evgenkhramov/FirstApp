using CoreGraphics;
using FirstApp.Core;
using FirstApp.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

namespace FirstApp.iOS.ViewControllers.Tasks
{
    [MvxTabPresentation(WrapInNavigationController = true, TabName = "Task List", TabIconName = "taskList")]
    public partial class TaskListViewController : MvxViewController<TaskListViewModel>
    {
        private MvxUIRefreshControl _refreshControl;
        public TaskListViewController () : base (nameof(TaskListViewController), null)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        public override void ViewDidLoad()
        {
            SetupNavigationBar();

            base.ViewDidLoad(); 

            Title = Constants.TaskList;

            EdgesForExtendedLayout = UIRectEdge.None;

            View.BackgroundColor = UIColor.Clear;
            _refreshControl = new MvxUIRefreshControl();

            TasksTable.RegisterNibForCellReuse(TaskCellViewController.Nib, TaskCellViewController.Key);
            var source = new TasksTVS(TasksTable);
            TasksTable.Source = source;
            TasksTable.AddSubview(_refreshControl);

            var set = this.CreateBindingSet<TaskListViewController, TaskListViewModel>();

            set.Bind(source).To(m => m.TaskCollection);
            set.Bind(source).For(v => v.SelectionChangedCommand).To(vm => vm.ShowTaskChangedView);
            set.Bind(source).For(s => s.DeleteRowCommandiOS).To(vm => vm.DeleteItemCommandiOS);

            set.Bind(_refreshControl).For(r => r.IsRefreshing).To(vm => vm.IsRefreshTaskCollection);
            set.Bind(_refreshControl).For(r => r.RefreshCommand).To(vm => vm.RefreshTaskCommand);

            set.Apply();           
        }

        private void SetupNavigationBar()
        {
            var _addTask = new UIButton(UIButtonType.Custom);
            _addTask.Frame = new CGRect(0, 0, 40, 40);
            _addTask.SetImage(UIImage.FromBundle("taskList"), UIControlState.Normal);

            NavigationItem.SetRightBarButtonItems(new UIBarButtonItem[] { new UIBarButtonItem(_addTask) }, false);

            var set = this.CreateBindingSet<TaskListViewController, TaskListViewModel>();
            set.Bind(_addTask).To(vm => vm.CreateNewTask);
            set.Apply();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }
    }
}