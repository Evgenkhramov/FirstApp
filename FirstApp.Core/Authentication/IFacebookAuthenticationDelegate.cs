using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FirstApp.Core.Authentication
{
    public interface IFacebookAuthenticationDelegate
    {
        Task OnAuthenticationCompleted(FacebookOAuthToken token);
        Task OnAuthenticationFailed();
        Task OnAuthenticationCanceled();
    }
}
