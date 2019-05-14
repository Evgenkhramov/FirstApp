using CoreGraphics;
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
            SetupNavigationBar();

            base.ViewDidLoad(); 

            Title = "Task List";

            EdgesForExtendedLayout = UIRectEdge.None;

            // NavigationController.NavigationBarHidden = true;

            View.BackgroundColor = UIColor.Clear;
            _refreshControl = new MvxUIRefreshControl();

            TasksTable.RegisterNibForCellReuse(TaskCell.Nib, TaskCell.Key);
            var source = new TasksTVS(TasksTable);
            TasksTable.Source = source;
            TasksTable.AddSubview(_refreshControl);

            var set = this.CreateBindingSet<TaskListController, TaskListViewModel>();
            //set.Bind(AddNewTaskButton).To(vm => vm.CreateNewTask);
            set.Bind(source).To(m => m.TaskCollection);
            set.Bind(source).For(v => v.SelectionChangedCommand).To(vm => vm.ShowTaskChangedView);
            set.Apply();           
        }

        private void SetupNavigationBar()
        {
            //var _backButton = new UIButton(UIButtonType.Custom);
            //_backButton.Frame = new CGRect(0, 0, 40, 40);
            //_backButton.SetImage(UIImage.FromBundle("Back"), UIControlState.Normal);

            var _addTask = new UIButton(UIButtonType.Custom);
            _addTask.Frame = new CGRect(0, 0, 40, 40);
            _addTask.SetImage(UIImage.FromBundle("taskList"), UIControlState.Normal);


            //_addTask.SetImage(UIImage.FromBundle("LogOutButton"), UIControlState.Normal);

            NavigationItem.SetRightBarButtonItems(new UIBarButtonItem[] { new UIBarButtonItem(_addTask) }, false);

            //UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(43, 61, 80);

             //_addTask.TouchUpInside += AddButtonClick;

            var set = this.CreateBindingSet<TaskListController, TaskListViewModel>();
            set.Bind(_addTask).To(vm => vm.CreateNewTask);
            set.Apply();
            //_logoutButton.TouchUpInside += LogoutButtonClick;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            //this.NavigationController.NavigationBarHidden = false;
            //this.NavigationController.NavigationItem.Title = "Title";
        }
    }
}