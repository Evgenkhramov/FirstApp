using FirstApp.Core.Entities;
using System.Collections.Generic;

namespace FirstApp.Core.Interfaces
{
    public interface IFileNameService
    {
        void AddFileNameToTable(FileListEntity fileName);
        List<FileListEntity> GetFileNameList(int taskId);
        void DeleteFileName(int id);
        void DeleteFiles(int taskId);
    }
}
