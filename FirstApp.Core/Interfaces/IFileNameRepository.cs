using FirstApp.Core.Entities;
using System.Collections.Generic;

namespace FirstApp.Core.Interfaces
{
    public interface IFileNameRepository
    {
        void InsertFileName(FileListEntity fileName);
        List<FileListEntity> GetFileNameList(int taskId);
        void DeleteFileNameList(int taskId);
        void DeleteFileName(int fileId);
    }
}
