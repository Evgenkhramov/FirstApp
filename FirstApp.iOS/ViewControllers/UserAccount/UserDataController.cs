using Acr.UserDialogs;
using FirstApp.Core.ViewModels;
using MvvmCross;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using System.Threading.Tasks;

namespace FirstApp.iOS.ViewControllers.UserAccount
{
    [MvxTabPresentation(WrapInNavigationController = true, TabName = "User Profile", TabIconName = "userProfile")]
    public partial class UserDataController : MvxViewController<UserDataViewModel>
    {
        public UserDataController() : base(nameof(UserDataController), null)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        public override void ViewDidLoad()
        {
            UserName.ShouldReturn = (textField) =>
            {
                textField.ResignFirstResponder();
                return true;
            };

            UserSurname.ShouldReturn = (textField) =>
            {
                textField.ResignFirstResponder();
                return true;
            };

            //NavigationController.NavigationBarHidden = true;

            var set = this.CreateBindingSet<UserDataController, UserDataViewModel>();
            set.Bind(UserName).To(vm => vm.UserName);
            set.Bind(UserSurname).To(vm => vm.Surname);
            set.Bind(UserImg).For(v => v.Image).To(vm => vm.MyPhoto).WithConversion("ByteArrayToImg");
            set.Bind(SaveUserButton).To(vm => vm.SaveUserData);
            set.Bind(CancelUserButton).To(vm => vm.Cancel);
            set.Bind(CameraButton).To(v => v. );

            set.Apply();

            base.ViewDidLoad();
        }

        public override void ViewWillAppear(bool animated)
        {

            //base.ViewWillAppear(animated);
            //this.NavigationController.NavigationBarHidden = false;
            //this.NavigationController.NavigationItem.Title = "Title";
        }

        public void ChoosePhoto()
        {
            SelectPhoto();
        }

        public async Task SelectPhoto()
        {
            bool answ = await Mvx.IoCProvider.Resolve<IUserDialogs>().ConfirmAsync("Please, select photo.", "Select Photo", "From memory", "From camera");

            if (answ)
            {
                ViewModel.ChoosePictureCommand.Execute(null);
            }
            if (!answ)
            {
                ViewModel.TakePictureCommand.Execute(null);
            }
        }
    }
}