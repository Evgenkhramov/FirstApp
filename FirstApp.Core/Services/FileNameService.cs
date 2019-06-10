using FirstApp.Core.Entities;
using FirstApp.Core.Interfaces;
using System.Collections.Generic;

namespace FirstApp.Core.Services
{
    public class FileNameService : IFileNameService
    {
        private readonly IFileNameRepository _fileNameRepositoryService;

        public FileNameService(IFileNameRepository fileNameRepositoryService)
        {
            _fileNameRepositoryService = fileNameRepositoryService;
        }

        public void AddFileNameToTable(FileListEntity fileName)
        {
            _fileNameRepositoryService.Insert(fileName);
        }

        public List<FileListEntity> GetFileNameList(int taskId)
        {
            List<FileListEntity> list = _fileNameRepositoryService.Get(taskId);

            return list;
        }

        public void DeleteFiles(int taskId)
        {
            _fileNameRepositoryService.DeleteFiles(taskId);
        }

        public void DeleteFileName(int fileId)
        {
            _fileNameRepositoryService.Delete(fileId);
        }
    }
}
