using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Server.Common.Models;
using Server.DataAccess.Interfaces;
using Server.Common.Enums;

namespace Server.DataAccess.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly string _uploadFolder;
        private readonly AppDbContext _db;

        public FileRepository(IConfiguration configuration, AppDbContext db)
        {
            var uploadFolder = configuration["UploadFolder"] ?? "UploadedFiles";
            _uploadFolder = Path.Combine(System.Environment.CurrentDirectory, uploadFolder);

            if (!Directory.Exists(_uploadFolder))
            {
                Directory.CreateDirectory(_uploadFolder);
            }

            _db = db;
        }

        public async Task<bool> SaveFilesAsync(List<IFormFile> files, Account account, Document document)
        {
            if (files == null || files.Count == 0)
                throw new ArgumentException("No files provided");

            var filePath = Path.Combine(_uploadFolder, account.Email);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            int docCount = 0;
            List<DocumentDetail> documentDetailsList = new();
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var uniqueKey = Guid.NewGuid().ToString();
                    var extension = Path.GetExtension(formFile.FileName);
                    var newFileName = uniqueKey + extension;

                    var newFilePath = Path.Combine(filePath, newFileName);

                    using var stream = new FileStream(newFilePath, FileMode.Create);
                    await formFile.CopyToAsync(stream);

                    docCount++;
                    DocumentDetail dt = new()
                    {
                        DocumentId = document.Id,
                        Name = $"Document {docCount}",
                        Status = DocumentStatus.Pending.ToString(),
                        DocumentLink = Path.Combine(account.Email, newFileName)
                    };
                    documentDetailsList.Add(dt);
                }
            }
            await _db.AddRangeAsync(documentDetailsList);
            await _db.SaveChangesAsync();

            return true;
        }
    }
}