using System;
using System.Collections.Generic;
using System.Text;

namespace FirstApp.Core.Authentication
{
    public interface IGoogleAuthenticationDelegate
    {
        void OnAuthenticationCompleted(GoogleOAuthToken token);
        void OnAuthenticationFailed(string message, Exception exception);
        void OnAuthenticationCanceled();
    }
}
