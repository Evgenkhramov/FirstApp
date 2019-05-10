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

            base.ViewDidLoad();

            // Keyboard popup
            NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.DidShowNotification, KeyBoardUpNotification);
            // Keyboard Down
            NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, KeyBoardDownNotification);

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

        private void KeyBoardUpNotification(NSNotification notification)
        {


            // get the keyboard size
            CGRect r = UIKeyboard.BoundsFromNotification(notification);

            GetActiveView(this.View);
            // Find what opened the keyboard
            //foreach (UIView view in this.View.Subviews)
            //{
            //    if (view.IsFirstResponder)
            //        activeview = view;
            //}

            // Bottom of the controller = initial position + height + offset      
            bottom = (activeview.Frame.Y + activeview.Frame.Height + offset);

            // Calculate how far we need to scroll
            scroll_amount = (r.Height - (View.Frame.Size.Height - bottom));

            // Perform the scrolling
            if (scroll_amount > 0)
            {
                moveViewUp = true;
                ScrollTheView(moveViewUp);
            }
            else
            {
                moveViewUp = false;
            }
        }
        private void KeyBoardDownNotification(NSNotification notification)
        {
            if (moveViewUp) { ScrollTheView(false); }
        }

        private void ScrollTheView(bool move)
        {

            // scroll the view up or down
            UIView.BeginAnimations(string.Empty, System.IntPtr.Zero);
            UIView.SetAnimationDuration(0.3);

            CGRect frame = View.Frame;

            if (move)
            {
                frame.Y -= scroll_amount;
            }
            else
            {
                frame.Y += scroll_amount;
                scroll_amount = 0;
            }

            View.Frame = frame;
            UIView.CommitAnimations();
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