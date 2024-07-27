using Microsoft.EntityFrameworkCore;
using Server.Common.Models;
using Server.DataAccess.Interfaces;

namespace Server.DataAccess.Repositories
{
    public class DocRepository : IDocRepository
    {
        private readonly AppDbContext _db;

        public DocRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<DocumentDetail>> GetDocDetails(Document document)
        {
            return await _db.DocumentDetails.Where(dt => dt.DocumentId == document.Id).ToListAsync();
        }

        public async Task<Document> GetLastDoc(Account account)
        {
            return await _db.Documents.Where(d => d.AccountId == account.Id).OrderByDescending(d => d.Timestamp).FirstOrDefaultAsync() ?? throw new Exception("Document not found");
        }
    }
}