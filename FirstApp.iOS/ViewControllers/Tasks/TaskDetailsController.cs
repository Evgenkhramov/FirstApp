using FirstApp.Core.ViewModels;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using System;

namespace FirstApp.iOS.ViewControllers.Tasks
{
    public partial class TaskDetailsController : MvxViewController<TaskDetailsViewModel>
    {
        public TaskDetailsController() : base(nameof(TaskDetailsController), null)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        public override void ViewDidLoad()
        {
            TaskName.ShouldReturn = (textField) =>
            {
                textField.ResignFirstResponder();
                return true;
            };

            TaskDescription.ShouldReturn = (textField) =>
            {
                textField.ResignFirstResponder();
                return true;
            };

            //NavigationController.NavigationBarHidden = true;

            var set = this.CreateBindingSet<TaskDetailsController, TaskDetailsViewModel>();
            set.Bind(TaskName).To(vm => vm.TaskName);
            set.Bind(TaskDescription).To(vm => vm.TaskDescription);
            set.Bind(AddFileInTaskButton).To(vm => vm.AddFile);
            set.Bind(AddMapMarkersButton).To(vm => vm.AddMarker);
            set.Bind(DeleteTaskButton).To(vm => vm.DeleteTask);
            set.Bind(MapMarkersCount).To(vm => vm.MapMarkers);
            set.Bind(SaveTaskButton).To(vm => vm.SaveTaskForiOS);
            
            //set.Bind(CameraButton).To(v => v. );

            set.Apply();

            base.ViewDidLoad();
        }

        public override void ViewWillAppear(bool animated)
        {

            //base.ViewWillAppear(animated);
            //this.NavigationController.NavigationBarHidden = false;
            //this.NavigationController.NavigationItem.Title = "Title";
        }
    }
}