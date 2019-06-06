using FirstApp.Core.Interfaces;
using FirstApp.Core.Models;
using System;
using Xamarin.Auth;

namespace FirstApp.Core.Providers
{
    public class FacebookAuthenticator : IDisposable
    {
        private const string AUTORIZE_URL = "https://www.facebook.com/dialog/oauth/";
        private const string REDIRECT_URL = "https://www.facebook.com/connect/login_success.html";
        private const bool IS_USING_NATIVE_UI = false;

        private OAuth2Authenticator _auth;

        private IFacebookAuthenticationDelegate _authenticationDelegate;

        public FacebookAuthenticator(string clientId, string scope, IFacebookAuthenticationDelegate authenticationDelegate)
        {
            _authenticationDelegate = authenticationDelegate;

            _auth = new OAuth2Authenticator(clientId, scope,
                                            new Uri(AUTORIZE_URL),
                                            new Uri(REDIRECT_URL),
                                            null, IS_USING_NATIVE_UI);

            _auth.Completed += OnAuthenticationCompleted;

            _auth.Error += OnAuthenticationFailed;
        }

        public OAuth2Authenticator GetAuthenticator()
        {
            return _auth;
        }

        public void OnPageLoading(Uri uri)
        {
            _auth.OnPageLoading(uri);
        }

        private void OnAuthenticationCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (e.IsAuthenticated)
            {
                var token = new FacebookOAuthToken
                {
                    AccessToken = e.Account.Properties[Constants.AccessToken]
                };

                _authenticationDelegate.OnAuthenticationCompleted(token);
            }

            if (!e.IsAuthenticated)
            {
                _authenticationDelegate.OnAuthenticationCanceled();
            }
        }

        private void OnAuthenticationFailed(object sender, AuthenticatorErrorEventArgs e)
        {
            _authenticationDelegate.OnAuthenticationFailed();
        }

        public void Dispose()
        {
            _auth.Error -= OnAuthenticationFailed;
            _auth.Completed -= OnAuthenticationCompleted;
        }
    }
}
