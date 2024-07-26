using Server.Models.DTOs.Resident;

namespace Server.BusinessLogic.Interfaces
{
    public interface IResidentService
    {
        Task<List<NewResidentResponse>> GetAllNewResidentRequest(string accessToken);
    }
}