using FirstApp.Core;
using FirstApp.Core.Interfaces;
using FirstApp.Core.Models;
using FirstApp.Core.Providers;
using FirstApp.Core.Services;
using FirstApp.Core.ViewModels;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using System;
using UIKit;

namespace FirstApp.iOS.ViewControllers.Authentication
{
    public partial class LoginViewController : MvxViewController<LoginViewModel>, IGoogleAuthenticationDelegate
    {
        #region Variables

        public static GoogleAuthenticator GoogleAuthenticator;

        private GoogleUserModeliOS _googleUser = new GoogleUserModeliOS();
        private FacebookAuthenticator _facebookAuthenticator;

        private static int GET_ACCOUNTS;

        #endregion Variables

        #region Constructors

        public LoginViewController() : base(nameof(LoginViewController), null)
        {
            GET_ACCOUNTS = 0;
        }

        #endregion Constructors

        #region Methods

        private void OnGoogleLoginButtonClicked(object sender, EventArgs e)
        {
            Xamarin.Auth.OAuth2Authenticator authentificator = GoogleAuthenticator.GetAuthenticator();
            UIViewController viewController = authentificator.GetUI();

            PresentViewController(viewController, true, null);
        }

        public async void OnAuthenticationCompleted(GoogleOAuthToken token)
        {
            DismissViewController(true, null);

            var googleService = new GoogleService();
            _googleUser = await googleService.GetUserProfileAsync(token.TokenType, token.AccessToken);

            await ViewModel.SaveUserGoogleiOS(_googleUser);
        }

        public void OnAuthenticationCanceled()
        {
            DismissViewController(true, null);

            var alertController = new UIAlertController
            {
                Title = Constants.CancelAuth,
                Message = Constants.DidNotComplite
            };

            alertController.AddAction(UIAlertAction.Create(Constants.CancelAuth, UIAlertActionStyle.Default, (UIAlertAction obj) =>
            {
            }));

            PresentViewController(alertController, true, null);

            DismissViewController(true, null);
        }

        public void OnAuthenticationFailed(string message, Exception exception)
        {
            DismissViewController(true, null);

            var alertController = new UIAlertController
            {
                Title = message,
                Message = exception?.ToString()
            };

            alertController.AddAction(UIAlertAction.Create(Constants.CancelAuth, UIAlertActionStyle.Default, (UIAlertAction obj) =>
            {
            }));

            PresentViewController(alertController, true, null);

            DismissViewController(true, null);
        }

        private void OnFacebookLoginButtonClicked(object sender, EventArgs e)
        {
            _facebookAuthenticator = new FacebookAuthenticator(Configuration.ClientId, Configuration.Scope, ViewModel);

            Xamarin.Auth.OAuth2Authenticator authenticator = _facebookAuthenticator.GetAuthenticator();
            UIViewController view = authenticator.GetUI();

            PresentViewController(view, true, null);
        }

        private void SetBind()
        {
            MvxFluentBindingDescriptionSet<LoginViewController, LoginViewModel> set = this.CreateBindingSet<LoginViewController, LoginViewModel>();

            set.Bind(EnterYourEmail).To(vm => vm.UserEmail);
            set.Bind(EnterYourPasswordForLogin).To(vm => vm.UserPassword);
            set.Bind(LoginButton).To(vm => vm.UserLogin);
            set.Bind(RegistrationButton).To(vm => vm.UserRegistration);

            set.Apply();
        }

        #endregion Methods

        #region Overrides

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            GoogleAuthenticator = new GoogleAuthenticator(Configuration.ClientIdGoogle, Configuration.GoogleScope, Configuration.iOSRedirectUrlGoogle, this);

            FacebookButton.TouchUpInside += OnFacebookLoginButtonClicked;

            GoogleButton.TouchUpInside += OnGoogleLoginButtonClicked;        

            EnterYourEmail.ShouldReturn = (textField) =>
            {
                textField.ResignFirstResponder();
                return true;
            };

            EnterYourPasswordForLogin.ShouldReturn = (textField) =>
            {
                textField.ResignFirstResponder();
                return true;
            };

            NavigationController.NavigationBarHidden = true;

            SetBind();
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            View.EndEditing(true);

            base.TouchesBegan(touches, evt);
        }

        public override void ViewDidUnload()
        {
            FacebookButton.TouchUpInside -= OnFacebookLoginButtonClicked;

            GoogleButton.TouchUpInside -= OnGoogleLoginButtonClicked;

            base.ViewDidUnload();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
        }

        #endregion Overrides
    }
}