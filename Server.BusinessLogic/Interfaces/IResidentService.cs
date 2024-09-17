using Server.Models.DTOs.Resident;

namespace Server.BusinessLogic.Interfaces
{
    public interface IResidentService
    {
        Task<List<NewResidentResponse>> GetAllNewResidentRequest();
        Task<DetailsNewResidentResponse> GetDetailsNewResident(string residentEmail);
        Task UpdateAccountStatus(long accountId, string Status);
    }
}