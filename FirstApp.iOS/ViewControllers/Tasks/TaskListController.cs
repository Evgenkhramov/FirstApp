using FirstApp.Core.ViewModels;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using System;

namespace FirstApp.iOS.ViewControllers
{
    public partial class TaskListController : MvxViewController<TaskListViewModel>
    {
        public TaskListController () : base (nameof(TaskListController), null)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        public override void ViewDidLoad()
        {
            SetBind();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            //this.NavigationController.NavigationBarHidden = false;
            //this.NavigationController.NavigationItem.Title = "Title";
        }

        private void SetBind()
        {
            var set = this.CreateBindingSet<TaskListController, TaskListViewModel>();
            set.Bind(AddNewTaskButton).To(vm => vm.CreateNewTask);

            set.Apply();
        }
    }
}