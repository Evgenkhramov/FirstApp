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
            _fileNameRepositoryService.InsertFileName(fileName);
        }

        public List<FileListEntity> GetFileNameList(int taskId)
        {
            List<FileListEntity> list = _fileNameRepositoryService.GetFileNameList(taskId);

            return list;
        }

        public void DeleteFiles(int taskId)
        {
            _fileNameRepositoryService.DeleteFileNameList(taskId);
        }

        public void DeleteFileName(int fileId)
        {
            _fileNameRepositoryService.DeleteFileName(fileId);
        }
    }
}
