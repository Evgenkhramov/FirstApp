using FirstApp.Core.ViewModels;
using MvvmCross.Platforms.Ios.Views;

namespace FirstApp.iOS.ViewControllers.Authentication
{
    public partial class StartViewController : MvxViewController<StartViewModel>
    {
        #region Constructors

        public StartViewController() : base(nameof(StartViewController), null)
        {
        }

        #endregion Constructors

        #region Overrides

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        public override void ViewDidLoad()
        {
            NavigationController.NavigationBarHidden = true;

            base.ViewDidLoad();

            ViewModel.ShowLoginFragmentCommand.Execute(null);
        }

        #endregion Overrides
    }
}