using FirstApp.Core.Models;
using System.Threading.Tasks;

namespace FirstApp.Core.Interfaces
{
    public interface IListHandler
    {
        Task CollectionItemClick(TaskModel model);
        void RemoveTaskCollectionItem(int itemId);
    }
}
