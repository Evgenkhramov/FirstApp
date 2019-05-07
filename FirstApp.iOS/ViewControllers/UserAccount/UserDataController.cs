using FirstApp.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;

namespace FirstApp.iOS.ViewControllers
{
    [MvxTabPresentation(WrapInNavigationController = true, TabName = "User Profile", TabIconName = "userProfile")]
    public partial class UserDataController : MvxViewController<UserDataViewModel>
    { 
        public UserDataController() : base(nameof(TaskListController), null)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        public override void ViewDidLoad()
        {
            NavigationController.NavigationBarHidden = true;

            var set = this.CreateBindingSet<UserDataController, UserDataViewModel>();
            set.Bind(UserName).To(vm => ViewModel.UserName);
            set.Bind(UserSurname).To(m => ViewModel.Surname);
            set.Bind(UserImg).To(m => ViewModel.MyPhoto).WithConversion("ByteArrayToImg");
            set.Bind(SaveUserButton).To(m => ViewModel.SaveUserData);
            set.Bind(CancelUserButton).To(m => ViewModel.Cancel);
            //set.Bind(CameraButton).To(m => ViewModel.);
           
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