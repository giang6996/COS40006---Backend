using Server.Common.Models;
using Server.DataAccess.Interfaces;

namespace Server.DataAccess.Repositories
{
    public class PropertyDossierRepository : IPropertyDossierRepository
    {
        private readonly AppDbContext _db;

        public PropertyDossierRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Document> CreateNewDoc(Module module, Account account)
        {
            Document document = new()
            {
                AccountId = account.Id,
                ModuleId = module.Id,
                Timestamp = DateTime.Now
            };

            await _db.Documents.AddAsync(document);
            await _db.SaveChangesAsync();

            return document;
        }
    }
}