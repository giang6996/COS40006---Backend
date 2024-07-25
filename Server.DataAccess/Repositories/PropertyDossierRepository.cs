using Server.Common.Models;
using Server.DataAccess.Interfaces;

namespace Server.DataAccess.Repositories
{
    public class PropertyDossier : IPropertyDossierRepository
    {
        private readonly AppDbContext _db;

        public PropertyDossier(AppDbContext db)
        {
            _db = db;
        }

        // public async Task<Document> CreateNewDoc()
        // {

        // }
    }
}