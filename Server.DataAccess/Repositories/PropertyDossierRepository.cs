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

        public async Task<Document> CreateNewDoc()
        {
            throw new Exception("");
        }
    }
}