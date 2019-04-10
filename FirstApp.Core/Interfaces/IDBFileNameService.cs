using FirstApp.Core.Models;

namespace FirstApp.Core.Interfaces
{
    public interface IDBFileNameService
    {
        void AddFileNameToTable(FileListModel fileName);
        string[] GetFileNameFromDB(int taskId);
    }
}
