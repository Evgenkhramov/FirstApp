using FirstApp.Core.Entities;
using FirstApp.Core.Interfaces;
using System.Collections.Generic;

namespace FirstApp.Core.Services
{
    public class FileNameService : IFileNameService
    {
        private readonly IFileNameRepository _fileNameRepository;

        public FileNameService(IFileNameRepository fileNameRepository)
        {
            _fileNameRepository = fileNameRepository;
        }

        public void AddFileNameToTable(FileListEntity fileName)
        {
            _fileNameRepository.Insert(fileName);
        }

        public List<FileListEntity> GetFileNameList(int taskId)
        {
            List<FileListEntity> list = _fileNameRepository.Get(taskId);

            return list;
        }

        public void DeleteFiles(int taskId)
        {
            _fileNameRepository.DeleteFiles(taskId);
        }

        public void DeleteFileName(int fileId)
        {
            _fileNameRepository.Delete(fileId);
        }
    }
}
