using FirstApp.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

namespace FirstApp.iOS.ViewControllers.Tasks
{
    [MvxTabPresentation(WrapInNavigationController = true, TabName = "Task List", TabIconName = "taskList")]
    public partial class TaskListController : MvxViewController<TaskListViewModel>
    {
        private MvxUIRefreshControl _refreshControl;
        public TaskListController () : base (nameof(TaskListController), null)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        public override void ViewDidLoad()
        {
            

           // NavigationController.NavigationBarHidden = true;

            View.BackgroundColor = UIColor.Clear;
            _refreshControl = new MvxUIRefreshControl();

            TasksTable.RegisterNibForCellReuse(TaskCell.Nib, TaskCell.Key);
            var source = new TasksTVS(TasksTable);
            TasksTable.Source = source;
            TasksTable.AddSubview(_refreshControl);

            var set = this.CreateBindingSet<TaskListController, TaskListViewModel>();
            set.Bind(AddNewTaskButton).To(vm => vm.CreateNewTask);
            set.Bind(source).To(m => m.TaskCollection);
            set.Bind(source).For(v => v.SelectionChangedCommand).To(vm => vm.ShowTaskChangedView);
            set.Apply();

            base.ViewDidLoad();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            //this.NavigationController.NavigationBarHidden = false;
            //this.NavigationController.NavigationItem.Title = "Title";
        }
    }
}