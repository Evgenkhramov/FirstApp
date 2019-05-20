using FirstApp.Core.Enums;
using FirstApp.Core.Interfaces;

namespace FirstApp.Droid.Services
{
    class GetCurrentPlatformService : IGetCurrentPlatformService
    {
        public CurrentPlatform CurrentPlatform()
        {
            CurrentPlatform answ = Core.Enums.CurrentPlatform.Android;

            return answ;
        }
    }
}