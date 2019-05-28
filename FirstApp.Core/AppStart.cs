using FirstApp.Core.Interfaces;
using FirstApp.Core.ViewModels;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.SecureStorage;
using System;
using System.Threading.Tasks;

namespace FirstApp.Core
{
    public class AppStart : MvxAppStart
    {
        #region Variables

        private readonly IMvxNavigationService _mvxNavigationService;
        private readonly IAuthorizationService _authorizationService;

        #endregion Variables

        #region Constructors

        public AppStart(IMvxApplication app, IMvxNavigationService mvxNavigationService,
                               IAuthorizationService loginService)
                   : base(app, mvxNavigationService)
        {
            _mvxNavigationService = mvxNavigationService;
            _authorizationService = loginService;
        }

        #endregion Constructors

        #region Overrides
        protected override Task NavigateToFirstViewModel(object hint = null)
        {
            string sequreKeyForLoged = CrossSecureStorage.Current.GetValue(Constants.SequreKeyForLoged);
            if (!String.IsNullOrEmpty(sequreKeyForLoged) && sequreKeyForLoged.Equals(Constants.LogIn))
            {
                return _mvxNavigationService.Navigate<MainViewModel>();
            }

            return _mvxNavigationService.Navigate<StartViewModel>();

            //return _mvxNavigationService.Navigate<StartViewModel>();
        }
        #endregion Overrides
    }
}
