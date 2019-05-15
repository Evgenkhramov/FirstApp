using System.Threading.Tasks;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using FirstApp.Core.ViewModels;
using Plugin.SecureStorage;
using FirstApp.Core.Interfaces;
using System;

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
            string sequreKeyForLoged = CrossSecureStorage.Current.GetValue(Constants.SequreKeyForLoged);
            if (!String.IsNullOrEmpty(sequreKeyForLoged) && sequreKeyForLoged.Equals(Constants.LogIn))
            {
                return _mvxNavigationService.Navigate<MainViewModel>();
            }
            else
            {
                return _mvxNavigationService.Navigate<StartViewModel>();
            }
           // return _mvxNavigationService.Navigate<StartViewModel>();
        }
    }
}
