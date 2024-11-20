using Server.Common.Models;

namespace Server.DataAccess.Interfaces
{
    public interface IPropertyDossierRepository
    {
        Task<Document> CreateNewDoc(Module module, long accountId, long buildingId, long apartmentId);
    }
}