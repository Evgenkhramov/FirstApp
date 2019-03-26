using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FirstApp.Core; 
using FirstApp.Core.Authentication;
using FirstApp.Core.ViewModels;
using FirstApp.Core.Authentication;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using FirstApp.Core.Services;

namespace FirstApp.Droid.Views
{
    [MvxFragmentPresentation(typeof(StartViewModel), Resource.Id.content_frame_new, false)]
    [Register("firstApp.Droid.Views.LoginFragment")]
    public class LoginFragment : BaseFragment<LoginFragmentViewModel>, IFacebookAuthenticationDelegate
    {
        protected override int FragmentId => Resource.Layout.LoginFragment;
        private FacebookAuthenticator _auth;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            _auth = new FacebookAuthenticator(Configuration.ClientId, Configuration.Scope, this);

            var facebookLoginButton = view.FindViewById<Button>(Resource.Id.facebookLoginButton);
            facebookLoginButton.Click += OnFacebookLoginButtonClicked;

            return view;
        }

        private void OnFacebookLoginButtonClicked(object sender, EventArgs e)
        {
            var authenticator = _auth.GetAuthenticator();
            var intent = authenticator.GetUI(this.Context);
            intent.SetFlags(ActivityFlags.NoHistory);
            StartActivity(intent);
            ParentActivity.Finish();
        }

        public async void OnAuthenticationCompleted(FacebookOAuthToken token)
        {
            // Retrieve the user's email address
            var facebookService = new FacebookService();
            var email = await facebookService.GetEmailAsync(token.AccessToken);
           
            ViewModel.ShowMainViewModelCommand.Execute();
            //ViewModel.AddUserToTable(email);
        }

        public void OnAuthenticationCanceled()
        {
            new AlertDialog.Builder(this.Context)
                           .SetTitle("Authentication canceled")
                           .SetMessage("You didn't completed the authentication process")
                           .Show();
        }

        public void OnAuthenticationFailed(string message, Exception exception)
        {
            new AlertDialog.Builder(this.Context)
                           .SetTitle(message)
                           .SetMessage(exception?.ToString())
                           .Show();
        }
    }

}