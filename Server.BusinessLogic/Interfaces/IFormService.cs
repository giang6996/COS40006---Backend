using Server.Common.DTOs;
using Server.Common.Enums;
using Server.Models.DTOs.Form;

namespace Server.BusinessLogic.Interfaces
{
    public interface IFormService
    {
        Task HandleNewRequest(FormResidentRequest request, string accessToken);
        Task<List<FormResponse>> HandleGetAllFormRequest(string accessToken, Permission userPermission, string? status, string? label, string? type);
        Task HandleAdminResponse(long id, string response, string status);
    }
}