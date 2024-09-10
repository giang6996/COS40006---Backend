using Server.Common.DTOs;
using Server.Models.DTOs.Form;

namespace Server.BusinessLogic.Interfaces
{
    public interface IFormService
    {
        Task HandleNewRequest(FormResidentRequest request, string accessToken);

        Task<List<FormResponse>> HandleGetAllFormRequest(string accessToken, string? status, string? label, string? type);
    }
}