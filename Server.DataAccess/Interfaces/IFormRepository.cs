using Server.Common.Models;

namespace Server.DataAccess.Interfaces
{
    public interface IFormRepository
    {
        Task AddNewFormAsync(FormResidentRequest formResidentRequest);
        Task AddNewFormDetailAsync(FormResidentRequestDetail formResidentRequestDetail);
    }
}