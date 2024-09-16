using Server.Common.DTOs;
using Server.Common.Models;

namespace Server.DataAccess.Interfaces
{
    public interface IFormRepository
    {
        Task AddNewFormAsync(FormResidentRequest formResidentRequest);
        Task AddNewFormDetailAsync(FormResidentRequestDetail formResidentRequestDetail);
        Task<List<FormResponse>> GetAllOwnForms(long accountId, string? status, string? label, string? type);
        Task<List<FormResponse>> GetAllTenantForms(long accountId, string? status, string? label, string? type);
        Task<List<FormResponse>> GetAllFormRequest(string? status, string? label, string? type);
        Task<FormResponse> GetFormRequestDetail(long id);
        Task UpdateFormResponse(long id, string response, string status);
    }
}