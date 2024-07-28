using Server.Common.Models;
using Server.Models.DTOs.Resident;

namespace Server.BusinessLogic.Interfaces
{
    public interface IResidentService
    {
        Task<List<NewResidentResponse>> GetAllNewResidentRequest(string accessToken);
        Task<DetailsNewResidentResponse> GetDetailsNewResident(string accessToken, string residentEmail);
        Task UpdateAccountStatus(string accessToken, long accountId, string Status);
    }
}