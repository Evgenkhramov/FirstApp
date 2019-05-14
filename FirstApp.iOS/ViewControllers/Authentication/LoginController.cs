using FirstApp.Core;
using FirstApp.Core.Authentication;
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
        public static GoogleAuthenticator _authGoogle;

        private FacebookAuthenticator _authFacebook;
        static readonly int GET_ACCOUNTS = 0;

        public LoginController() : base()
        {
        }
        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _authGoogle = new GoogleAuthenticator(Configuration.ClientIdGoogle, Configuration.Scope, Configuration.iOSRedirectUrlGoogle, this);

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
            var email = await googleService.GetEmailAsync(token.TokenType, token.AccessToken);
            //ViewModel. AddUserToTable(email);
            ViewModel.Execute(null);
        }

        public void OnAuthenticationCanceled()
        {
            DismissViewController(true, null);
            var alertController = new UIAlertController
            {
                Title = "Authentication Canceled",
                Message = "You didn`t completed the authentication process"

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
            //intent.SetFlags(ActivityFlags.NoHistory);
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
            //set.Bind(LoginButton).For(v=> v.Enabled).To(vm => vm.UserLogin);
            //set.Bind(LoginButton).For(v=> v.Enabled).To(vm => vm.UserLogin);

            set.Apply();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }
    }
}