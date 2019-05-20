using FirstApp.Core;
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
    public partial class LoginController : MvxViewController<LoginViewModel>, IGoogleAuthenticationDelegate
    {
        private GoogleModeliOS user = new GoogleModeliOS();
        public static GoogleAuthenticator _authGoogle;

        private FacebookAuthenticator _authFacebook;
        static int GET_ACCOUNTS;

        public LoginController() : base()
        {
            GET_ACCOUNTS = 0;
        }
        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _authGoogle = new GoogleAuthenticator(Configuration.ClientIdGoogle, Configuration.GoogleScope, Configuration.iOSRedirectUrlGoogle, this);

            FacebookButton.TouchUpInside += OnFacebookLoginButtonClicked;

            GoogleButton.TouchUpInside += OnGoogleLoginButtonClicked;

            EnterYourLogin.ShouldReturn = (textField) => 
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

        private void OnGoogleLoginButtonClicked(object sender, EventArgs e)
        {            
            var authentificator = _authGoogle.GetAuthenticator();
            var viewController = authentificator.GetUI();
            PresentViewController(viewController, true, null);
        }

        public async void OnAuthenticationCompleted(GoogleOAuthToken token)
        {
            DismissViewController(true, null);

            var googleService = new GoogleService();
            user = await googleService.GetUserProfileAsync(token.TokenType, token.AccessToken);

            await ViewModel.SaveUserGoogleiOS(user);
        }

        public void OnAuthenticationCanceled()
        {
            DismissViewController(true, null);
            var alertController = new UIAlertController
            {
                Title = Constants.CencelAuth,
                Message = Constants.DidNotComplite
            };
            alertController.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Default, (UIAlertAction obj) =>
            {
            }));
            PresentViewController(alertController, true, null);

            DismissViewController(true, null);
        }

        public void OnAuthenticationFailed(string message, Exception exception)
        {
            // SFSafariViewController doesn't dismiss itself
            DismissViewController(true, null);

            var alertController = new UIAlertController
            {
                Title = message,
                Message = exception?.ToString()
            };
            alertController.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Default, (UIAlertAction obj) =>
            {
            }));
            PresentViewController(alertController, true, null);

            DismissViewController(true, null);
        }

        private void OnFacebookLoginButtonClicked(object sender, EventArgs e)
        {
            _authFacebook = new FacebookAuthenticator(Configuration.ClientId, Configuration.Scope, ViewModel);
            var authenticator = _authFacebook.GetAuthenticator();
            var ui = authenticator.GetUI();
            
            PresentViewController(ui, true, null);
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            // hide the keyboard from all views
            View.EndEditing(true);

            base.TouchesBegan(touches, evt);
        }

        private void SetBind()
        {
            var set = this.CreateBindingSet<LoginController, LoginViewModel>();
            set.Bind(EnterYourLogin).To(vm => vm.UserName);
            set.Bind(EnterYourPasswordForLogin).To(vm => vm.UserPassword);
            set.Bind(LoginButton).To(vm => vm.UserLogin);
            set.Bind(RegistrationButton).To(vm => vm.UserRegistration);

            set.Apply();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }
    }
}