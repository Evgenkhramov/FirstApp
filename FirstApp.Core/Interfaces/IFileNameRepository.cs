using FirstApp.Core.Entities;
using System.Collections.Generic;

namespace FirstApp.Core.Interfaces
{
    public interface IFileNameRepository : IBaseRepository<FileListEntity>
    {
        List<FileListEntity> Get(int taskId);
        void DeleteFiles(int taskId);
        void Delete(int fileId);
    }
}
