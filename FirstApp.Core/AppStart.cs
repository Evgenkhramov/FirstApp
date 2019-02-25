using System.Threading.Tasks;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using FirstApp.Core.ViewModels;
using FirstApp.Core.Services;
using Plugin.SecureStorage;

namespace FirstApp.Core
{
    public class AppStart : MvxAppStart
    {
        private readonly IMvxNavigationService _mvxNavigationService;
        private readonly IAuthorizationService _authorizationService;

        public AppStart(IMvxApplication app,
                        IMvxNavigationService mvxNavigationService,
                        IAuthorizationService loginService)
            : base(app, mvxNavigationService)
        {
            _mvxNavigationService = mvxNavigationService;
            _authorizationService = loginService;
        }

        protected override Task NavigateToFirstViewModel(object hint = null)
        {
            if (CrossSecureStorage.Current.GetValue(Constants.SequreKeyForLoged)=="true")
            {
                return _mvxNavigationService.Navigate<MainViewModel>();
            }
            else
            {
                return _mvxNavigationService.Navigate<LoginViewModel>();
            }
        }

    }
}
