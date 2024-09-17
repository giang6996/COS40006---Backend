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
            var uploadFolder = configuration["UploadFolder"] ?? throw new Exception("UploadFolder configuration string is not valid");
            _uploadFolder = Path.Combine(System.Environment.CurrentDirectory, uploadFolder);
            _db = db;
        }

        public async Task<bool> SaveFilesAsync(List<IFormFile> files, long accountId, Document document)
        {
            if (files == null || files.Count == 0)
                return false;

            var filePath = Path.Combine(_uploadFolder, accountId.ToString());
            Directory.CreateDirectory(filePath); // Create directory if not exists

            var documentDetailsList = new List<DocumentDetail>();
            var tasks = new List<Task>();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var uniqueFileName = GenerateUniqueFileName(formFile.FileName);
                    var newFilePath = Path.Combine(filePath, uniqueFileName);

                    // Task to copy file
                    var copyTask = CopyFileAsync(formFile, newFilePath);
                    tasks.Add(copyTask);

                    // Create DocumentDetail
                    documentDetailsList.Add(new DocumentDetail
                    {
                        DocumentId = document.Id,
                        Name = $"Document {documentDetailsList.Count + 1}", // Incrementing based on the list
                        Status = DocumentStatus.Pending.ToString(),
                        DocumentLink = Path.Combine(accountId.ToString(), uniqueFileName)
                    });
                }
            }

            await Task.WhenAll(tasks); // Wait for all file operations to complete
            await _db.AddRangeAsync(documentDetailsList);
            await _db.SaveChangesAsync();

            return true;
        }

        // Helper methods
        private static string GenerateUniqueFileName(string originalFileName)
        {
            var uniqueKey = Guid.NewGuid().ToString();
            var extension = Path.GetExtension(originalFileName);
            return uniqueKey + extension;
        }

        private static async Task CopyFileAsync(IFormFile formFile, string filePath)
        {
            using var stream = new FileStream(filePath, FileMode.Create);
            await formFile.CopyToAsync(stream);
        }
    }
}