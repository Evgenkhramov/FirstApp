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
        #region Variables

        private MvxUIRefreshControl _refreshControl;
        private TasksTVS _source;

        #endregion Variables

        #region Constructors

        public TaskListViewController() : base(nameof(TaskListViewController), null)
        {
            Title = Constants.TaskList;

            EdgesForExtendedLayout = UIRectEdge.None;

            View.BackgroundColor = UIColor.Clear;
        }

        #endregion Constructors

        #region Methods

        private void SetBind()
        {
            MvxFluentBindingDescriptionSet<TaskListViewController, TaskListViewModel> set = this.CreateBindingSet<TaskListViewController, TaskListViewModel>();

            set.Bind(_source).To(m => m.TaskCollection);
            set.Bind(_source).For(v => v.SelectionChangedCommand).To(vm => vm.ShowTaskChangedView);
            set.Bind(_source).For(s => s.DeleteRowCommandiOS).To(vm => vm.DeleteItemCommandiOS);

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

            MvxFluentBindingDescriptionSet<TaskListViewController, TaskListViewModel> set = this.CreateBindingSet<TaskListViewController, TaskListViewModel>();
            set.Bind(_addTask).To(vm => vm.CreateNewTask);
            set.Apply();
        }

        #endregion Methods

        #region Overrides

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        public override void ViewDidLoad()
        {
            SetupNavigationBar();

            _source = new TasksTVS(TasksTable);

            SetBind();

            base.ViewDidLoad();

            _refreshControl = new MvxUIRefreshControl();

            TasksTable.RegisterNibForCellReuse(TaskCellViewController.TaskNib, TaskCellViewController.TaskKey);     
            
            TasksTable.Source = _source;

            TasksTable.AddSubview(_refreshControl);  
        }

        public override void ViewWillAppear(bool animated)
        {
            TabBarController.TabBar.Hidden = false;

            base.ViewWillAppear(animated);
        }

        #endregion Overrides
    }
}