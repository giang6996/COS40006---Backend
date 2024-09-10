using Server.Common.DTOs;
using Server.Common.Models;

namespace Server.DataAccess.Interfaces
{
    public interface IFormRepository
    {
        Task AddNewFormAsync(FormResidentRequest formResidentRequest);
        Task AddNewFormDetailAsync(FormResidentRequestDetail formResidentRequestDetail);
        Task<List<FormResponse>> GetAllFormRequestByAccount(Account account, string? status, string? label, string? type);
        Task<List<FormResponse>> GetAllFormRequest(string? status, string? label, string? type);
    }
}