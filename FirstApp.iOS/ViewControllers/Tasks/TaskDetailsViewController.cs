using CoreGraphics;
using FirstApp.Core;
using FirstApp.Core.ViewModels;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using System;
using UIKit;

namespace FirstApp.iOS.ViewControllers.Tasks
{
    public partial class TaskDetailsViewController : MvxViewController<TaskDetailsViewModel>
    {
        private MvxUIRefreshControl _refreshControl;
        public UIView activeview;             // Controller that activated the keyboard
        public nfloat scroll_amount = 0.0f;    // amount to scroll 
        public nfloat bottom = 0.0f;           // bottom point
        public nfloat offset = 10.0f;          // extra offset
        private bool moveViewUp = false;

        public TaskDetailsViewController() : base(nameof(TaskDetailsViewController), null)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        public override void ViewDidLoad()
        {


            base.ViewDidLoad();
            //HidesBottomBarWhenPushed = true;
            

            SetupNavigationBar();

            Title = Constants.TaskDetails;

            //EdgesForExtendedLayout = UIRectEdge.None;

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

            _refreshControl = new MvxUIRefreshControl();

            FileTabelView.RegisterNibForCellReuse(FileItemCellViewController.Nib, FileItemCellViewController.Key);
            var source = new FileTVS(FileTabelView);
            FileTabelView.Source = source;

            AddFileInTaskButton.TouchUpInside += (sender, e) =>
             {
                 OpenFile(sender, e);
             };

            var set = this.CreateBindingSet<TaskDetailsViewController, TaskDetailsViewModel>();
            set.Bind(TaskName).To(vm => vm.TaskName);
            set.Bind(source).To(vm => vm.FileNameList);
            set.Bind(source).For(s => s.DeleteRowCommandiOS).To(vm => vm.DeleteFileItemCommand);
            set.Bind(TaskDescription).To(vm => vm.TaskDescription);
            set.Bind(AddMapMarkersButton).To(vm => vm.AddMarkerCommand);
            set.Bind(DeleteTaskButton).To(vm => vm.DeleteTask);
            set.Bind(MapMarkersCount).To(vm => vm.MapMarkers);
            set.Bind(SaveTaskButton).To(vm => vm.SaveTask);

            set.Apply();
        }

        public async void OpenFile(object sender, EventArgs e)
        {
            string fileName = null;
            try
            {
                FileData fileData = await CrossFilePicker.Current.PickFile();
                if (fileData == null)
                    return; // user canceled file picking

                fileName = fileData.FileName;

            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Exception choosing file: " + ex.ToString());
            }

            if (!string.IsNullOrEmpty(fileName))
            {
                ViewModel.SaveFileName(fileName);
            }
        }

        private void SetupNavigationBar()
        {
            var _backButton = new UIButton(UIButtonType.Custom);
            _backButton.Frame = new CGRect(0, 0, 40, 40);
            _backButton.SetImage(UIImage.FromBundle("backButton"), UIControlState.Normal);

            NavigationItem.SetLeftBarButtonItems(new UIBarButtonItem[] { new UIBarButtonItem(_backButton) }, false);

            var set = this.CreateBindingSet<TaskDetailsViewController, TaskDetailsViewModel>();
            set.Bind(_backButton).To(vm => vm.BackCommand);
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

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            // hide the keyboard from all views
            View.EndEditing(true);

            base.TouchesBegan(touches, evt);
        }

        public override void ViewWillAppear(bool animated)
        {
            TabBarController.TabBar.Hidden = true;
            base.ViewWillAppear(animated);
        }
    }
}