using FirstApp.Core.Models;
using System.Threading.Tasks;

namespace FirstApp.Core.Interfaces
{
    public interface IFacebookService
    {
        Task<FacebookModel> GetUserDataAsync(string accessToken);
        Task<string> GetImageFromUrlToBase64(string url);
    }
}
