using Server.Models.DTOs.Form;

namespace Server.BusinessLogic.Interfaces
{
    public interface IFormService
    {
        Task HandleNewRequest(FormResidentRequest request, string accessToken);
    }
}