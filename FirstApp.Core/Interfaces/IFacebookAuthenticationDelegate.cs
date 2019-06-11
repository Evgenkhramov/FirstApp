using FirstApp.Core.Models;
using System.Threading.Tasks;

namespace FirstApp.Core.Interfaces
{
    public interface IFacebookAuthenticationDelegate
    {
        Task ProcessAuthenticationCompleted(FacebookOAuthToken token);
        Task ProcessAuthenticationFailed();
        Task ProcessAuthenticationCanceled();
    }
}
