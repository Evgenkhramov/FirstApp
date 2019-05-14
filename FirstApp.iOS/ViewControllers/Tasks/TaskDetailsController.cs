using CoreGraphics;
using FirstApp.Core.ViewModels;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using System;
using System.Drawing;
using UIKit;

namespace FirstApp.iOS.ViewControllers.Tasks
{
    public partial class TaskDetailsController : MvxViewController<TaskDetailsViewModel>
    {

        public UIView activeview;             // Controller that activated the keyboard
        public nfloat scroll_amount = 0.0f;    // amount to scroll 
        public nfloat bottom = 0.0f;           // bottom point
        public nfloat offset = 10.0f;          // extra offset
        private bool moveViewUp = false;

        public TaskDetailsController() : base(nameof(TaskDetailsController), null)
        {
            // activeview = View;
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        public override void ViewDidLoad()
        {
            SetupNavigationBar();
            base.ViewDidLoad();

            Title = "Task Details";

            EdgesForExtendedLayout = UIRectEdge.None;


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

        }

        private void SetupNavigationBar()
        {
            //var _backButton = new UIButton(UIButtonType.Custom);
            //_backButton.Frame = new CGRect(0, 0, 40, 40);
            //_backButton.SetImage(UIImage.FromBundle("Back"), UIControlState.Normal);

            var _backButton = new UIButton(UIButtonType.Custom);
            _backButton.Frame = new CGRect(0, 0, 40, 40);
            _backButton.SetImage(UIImage.FromBundle("backButton"), UIControlState.Normal);


            //_addTask.SetImage(UIImage.FromBundle("LogOutButton"), UIControlState.Normal);

            NavigationItem.SetLeftBarButtonItems(new UIBarButtonItem[] { new UIBarButtonItem(_backButton) }, false);

            //UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(43, 61, 80);

            //_addTask.TouchUpInside += AddButtonClick;

            var set = this.CreateBindingSet<TaskDetailsController, TaskDetailsViewModel>();
            set.Bind(_backButton).To(vm => vm.BackCommand);
            set.Apply();
            //_logoutButton.TouchUpInside += LogoutButtonClick;
        }

        public void GetActiveView(UIView view)
        {
            foreach (UIView item in view.Subviews)
            {
                if (view.IsFirstResponder)
                {
                    activeview = item;
                    return;
                }
                else
                    GetActiveView(item);
            }
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            // hide the keyboard from all views
            View.EndEditing(true);

            base.TouchesBegan(touches, evt);
        }

        public override void ViewWillAppear(bool animated)
        {

            //base.ViewWillAppear(animated);
            //this.NavigationController.NavigationBarHidden = false;
            //this.NavigationController.NavigationItem.Title = "Title";
        }
    }
}