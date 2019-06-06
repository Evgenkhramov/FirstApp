using FirstApp.Core.Entities;
using System.Collections.Generic;

namespace FirstApp.Core.Interfaces
{
    public interface IDBFileNameService
    {
        void AddFileNameToTable(FileListModel fileName);
        List<FileListModel> GetFileNameListFromDB(int taskId);
        void DeleteFileName(int id);
        void DeleteFiles(int taskId);
    }
}
