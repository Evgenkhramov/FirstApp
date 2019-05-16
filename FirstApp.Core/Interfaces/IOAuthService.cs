using Acr.UserDialogs;
using System.Threading.Tasks;

namespace FirstApp.Core.Interfaces
{
    public interface IOAuthService
    {
        Task<LoginResult> Login();
        void Logout();
    }
}
