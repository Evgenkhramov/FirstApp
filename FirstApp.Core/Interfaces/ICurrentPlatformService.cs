using FirstApp.Core.Models;

namespace FirstApp.Core.Interfaces
{
    public interface ICurrentPlatformService
    {
        CurrentPlatformType GetCurrentPlatform();
    }
}
