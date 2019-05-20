using FirstApp.Core.Enums;
using FirstApp.Core.Interfaces;

namespace FirstApp.iOS.Services
{
    class GetCurrentPlatformService : IGetCurrentPlatformService
    {
        public CurrentPlatform CurrentPlatform()
        {
            CurrentPlatform answ = Core.Enums.CurrentPlatform.iOS;

            return answ;
        }
    }
}