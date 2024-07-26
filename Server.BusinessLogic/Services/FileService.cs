using Microsoft.AspNetCore.Http;
using Server.BusinessLogic.Interfaces;
using Server.Common.Models;
using Server.DataAccess.Interfaces;

namespace Server.BusinessLogic.Services
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;

        public FileService(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<bool> UploadFileAsync(List<IFormFile> files, Account account, Document document)
        {
            if (files == null || files.Count == 0)
                throw new Exception("Invalid file");

            return await _fileRepository.SaveFilesAsync(files, account, document);
        }
    }
}