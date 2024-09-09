using Server.Common.Models;

namespace Server.DataAccess.Interfaces
{
    public interface IPropertyDossierRepository
    {
        Task<Document> CreateNewDoc(Module module, Account account, int roomNumber, string buildingName, string buildingAddress);
    }
}