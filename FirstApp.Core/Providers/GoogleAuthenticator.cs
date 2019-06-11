using FirstApp.Core.Interfaces;
using FirstApp.Core.Models;
using System;
using Xamarin.Auth;

namespace FirstApp.Core.Providers
{
    public class GoogleAuthenticator : IDisposable
    {
        private const string AUTORIZE_URL = "https://accounts.google.com/o/oauth2/v2/auth";
        private const string ACCESS_TOKEN_URL = "https://www.googleapis.com/oauth2/v4/token";
        private const bool IS_USING_NATIVE_UI = true;

        private readonly OAuth2Authenticator _authAuthenticator;
        private IGoogleAuthenticationDelegate _authenticationDelegate;

        public GoogleAuthenticator(string clientId, string scope, string redirectUrl, IGoogleAuthenticationDelegate authenticationDelegate)
        {
            _authenticationDelegate = authenticationDelegate;

            _authAuthenticator = new OAuth2Authenticator(clientId, string.Empty, scope,
                                            new Uri(AUTORIZE_URL),
                                            new Uri(redirectUrl),
                                            new Uri(ACCESS_TOKEN_URL),
                                            null, IS_USING_NATIVE_UI);

            _authAuthenticator.Completed += ProcessAuthenticationCompleted;

            _authAuthenticator.Error += ProcessAuthenticationFailed;
        }

        public OAuth2Authenticator GetAuthenticator()
        {
            return _authAuthenticator;
        }

        public void ProcessPageLoading(Uri uri)
        {
            _authAuthenticator.OnPageLoading(uri);
        }

        private void ProcessAuthenticationCompleted(object sender, AuthenticatorCompletedEventArgs eventArgs)
        {
            if (!eventArgs.IsAuthenticated)
            {
                _authenticationDelegate.ProcessAuthenticationCanceled();
                return;
            }

            var token = new GoogleOAuthToken
            {
                TokenType = eventArgs.Account.Properties[Constants.TokenType],
                AccessToken = eventArgs.Account.Properties[Constants.AccessToken]
            };

            _authenticationDelegate.ProcessAuthenticationCompleted(token);
        }

        private void ProcessAuthenticationFailed(object sender, AuthenticatorErrorEventArgs eventArgs)
        {
            _authenticationDelegate.ProcessAuthenticationFailed(eventArgs.Message, eventArgs.Exception);      
        }

        public void Dispose()
        {
            _authAuthenticator.Completed -= ProcessAuthenticationCompleted;
            _authAuthenticator.Error -= ProcessAuthenticationFailed;
        }
    }
}
