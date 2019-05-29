using FirstApp.Core.ViewModels;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;

namespace FirstApp.iOS.ViewControllers
{
    [MvxRootPresentation]
    public partial class MainViewController : MvxTabBarViewController<MainViewModel>
    {
        #region Variables

        private bool _firstTimePresented = true;

        #endregion Variables

        #region Constructors

        public MainViewController()
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
            base.ViewDidLoad();
        }

        public override void ViewWillAppear(bool animated)
        {
            if (_firstTimePresented)
            {
                _firstTimePresented = false;
                ViewModel.ShowMainFragmentCommand.Execute(null);
                ViewModel.ShowUserProfileViewModelCommand.Execute(null);
            }
        }

        #endregion Overrides
    }
}