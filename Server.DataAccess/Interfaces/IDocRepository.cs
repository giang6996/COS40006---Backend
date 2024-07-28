using Server.Common.Models;

namespace Server.DataAccess.Interfaces
{
    public interface IDocRepository
    {
        Task<Document> GetLastDoc(Account account);
        Task<List<DocumentDetail>> GetDocDetails(Document document);
        Task<Document> GetDocumentByAccountId(long accountId);
    }
}