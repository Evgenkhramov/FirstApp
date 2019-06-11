using FirstApp.Core.Models;
using System;

namespace FirstApp.Core.Interfaces
{
    public interface IGoogleAuthenticationDelegate
    {
        void ProcessAuthenticationCompleted(GoogleOAuthToken token);
        void ProcessAuthenticationFailed(string message, Exception exception);
        void ProcessAuthenticationCanceled();
    }
}
