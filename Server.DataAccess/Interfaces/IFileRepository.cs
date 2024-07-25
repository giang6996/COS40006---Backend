using Microsoft.AspNetCore.Http;
using Server.Common.Models;

namespace Server.DataAccess.Interfaces
{
    public interface IFileRepository
    {
        Task<bool> SaveFilesAsync(List<IFormFile> file, Account account);
    }
}