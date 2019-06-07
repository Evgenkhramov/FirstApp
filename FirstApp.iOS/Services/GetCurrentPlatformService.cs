using FirstApp.Core.Interfaces;
using FirstApp.Core.Models;

namespace FirstApp.iOS.Services
{
    public class CurrentPlatformService : ICurrentPlatformService
    {
        public CurrentPlatformType GetCurrentPlatform()
        {
            return CurrentPlatformType.iOS;
        }
    }
}