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

        private readonly OAuth2Authenticator _authAuthentificator;

        private IFacebookAuthenticationDelegate _authenticationDelegate;

        public FacebookAuthenticator(string clientId, string scope, IFacebookAuthenticationDelegate authenticationDelegate)
        {
            _authenticationDelegate = authenticationDelegate;

            _authAuthentificator = new OAuth2Authenticator(clientId, scope,
                                            new Uri(AUTORIZE_URL),
                                            new Uri(REDIRECT_URL),
                                            null, IS_USING_NATIVE_UI);

            _authAuthentificator.Completed += ProcessAuthenticationCompleted;

            _authAuthentificator.Error += ProcessAuthenticationFailed;
        }

        public OAuth2Authenticator GetAuthenticator()
        {
            return _authAuthentificator;
        }

        public void ProcessPageLoading(Uri uri)
        {
            _authAuthentificator.OnPageLoading(uri);
        }

        private void ProcessAuthenticationCompleted(object sender, AuthenticatorCompletedEventArgs eventArgument)
        {
            if (!eventArgument.IsAuthenticated)
            {
                _authenticationDelegate.ProcessAuthenticationCanceled();
                return;
            }

            var token = new FacebookOAuthToken
            {
                AccessToken = eventArgument.Account.Properties[Constants.AccessToken]
            };

            _authenticationDelegate.ProcessAuthenticationCompleted(token);
        }

        private void ProcessAuthenticationFailed(object sender, AuthenticatorErrorEventArgs errorEvent)
        {
            _authenticationDelegate.ProcessAuthenticationFailed();
        }

        public void Dispose()
        {
            _authAuthentificator.Error -= ProcessAuthenticationFailed;
            _authAuthentificator.Completed -= ProcessAuthenticationCompleted;
        }
    }
}
