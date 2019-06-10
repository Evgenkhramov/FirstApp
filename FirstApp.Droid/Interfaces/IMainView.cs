using System.Threading.Tasks;

namespace FirstApp.Droid.Interfaces
{
    public interface IMainView
    {       
        Task CloseDrawer();
        Task OpenDrawer();
    }
}