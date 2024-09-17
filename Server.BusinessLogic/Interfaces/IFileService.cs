using Microsoft.AspNetCore.Http;
using Server.Common.Models;

namespace Server.BusinessLogic.Interfaces
{
    public interface IFileService
    {
        Task<bool> UploadFileAsync(List<IFormFile> files, long accountId, Document document);
    }
}