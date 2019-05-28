using FirstApp.Core.Interfaces;
using FirstApp.Core.Models;

namespace FirstApp.Droid.Services
{
    public class CurrentPlatformService : ICurrentPlatformService
    {
        public CurrentPlatformType GetCurrentPlatform()
        {
            CurrentPlatformType answ = CurrentPlatformType.Android;

            return answ;
        }
    }
}