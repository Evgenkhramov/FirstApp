using FirstApp.Core.Models;
using System.Threading.Tasks;

namespace FirstApp.Core.Interfaces
{
    public interface IFacebookAuthenticationDelegate
    {
        Task OnAuthenticationCompleted(FacebookOAuthToken token);
        Task OnAuthenticationFailed();
        Task OnAuthenticationCanceled();
    }
}
