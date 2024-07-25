using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Server.Common.Models;
using Server.DataAccess.Interfaces;

namespace Server.DataAccess.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly string _uploadFolder;

        public FileRepository(IConfiguration configuration)
        {
            var uploadFolder = configuration["UploadFolder"] ?? "UploadedFiles";
            _uploadFolder = Path.Combine(System.Environment.CurrentDirectory, uploadFolder);

            if (!Directory.Exists(_uploadFolder))
            {
                Directory.CreateDirectory(_uploadFolder);
            }
        }

        public async Task<bool> SaveFilesAsync(List<IFormFile> files, Account account)
        {
            if (files == null || files.Count == 0)
                throw new ArgumentException("No files provided");

            var filePath = Path.Combine(_uploadFolder, account.Email);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var uniqueKey = Guid.NewGuid().ToString();
                    var extension = Path.GetExtension(formFile.FileName);
                    var newFileName = uniqueKey + extension;

                    filePath = Path.Combine(filePath, newFileName);

                    using var stream = new FileStream(filePath, FileMode.Create);
                    await formFile.CopyToAsync(stream);
                }
            }
            return true;
        }
    }
}