using System;
using System.Collections.Generic;
using System.Text;

namespace FirstApp.Core.Authentication
{
    public interface IFacebookAuthenticationDelegate
    {
        void OnAuthenticationCompleted(FacebookOAuthToken token);
        void OnAuthenticationFailed(string message, Exception exception);
        void OnAuthenticationCanceled();
    }
}
