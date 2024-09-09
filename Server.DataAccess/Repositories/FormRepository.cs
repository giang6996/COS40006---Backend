using Server.Common.Models;
using Server.DataAccess.Interfaces;

namespace Server.DataAccess.Repositories
{
    public class FormRepository : IFormRepository
    {
        private readonly AppDbContext _db;

        public FormRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddNewFormAsync(FormResidentRequest formResidentRequest)
        {
            _db.FormResidentRequests.Add(formResidentRequest);
            await _db.SaveChangesAsync();
        }

        public async Task AddNewFormDetailAsync(FormResidentRequestDetail formResidentRequestDetail)
        {
            _db.FormResidentRequestDetails.Add(formResidentRequestDetail);
            await _db.SaveChangesAsync();
        }

    }
}