using FirstApp.Core.Models;
using System.Threading.Tasks;

namespace FirstApp.Core.Interfaces
{
    public interface IListHandler
    {
        Task ClickOnCollectionItem(TaskRequestModel model);
        void RemoveTaskCollectionItem(int itemId);
    }
}
