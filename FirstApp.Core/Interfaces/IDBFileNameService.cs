using FirstApp.Core.Models;
using System.Collections.Generic;

namespace FirstApp.Core.Interfaces
{
    public interface IDBFileNameService
    {
        void AddFileNameToTable(FileListModel fileName);
        List<FileListModel> GetFileNameFromDB(int taskId);
        void DeleteFileName(int id);
    }
}
