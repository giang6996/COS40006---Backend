using Microsoft.EntityFrameworkCore;
using Server.Common.Models;
using Server.DataAccess.Interfaces;

namespace Server.DataAccess.Repositories
{
    public class ResidentRepository : IResidentRepository
    {
        private readonly AppDbContext _db;

        public ResidentRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Account>> FetchAllNewAccountsWithDocsSubmitted()
        {
            return await (
                from a in _db.Accounts
                join d in _db.Documents on a.Id equals d.AccountId
                group a by a into g
                where g.Count() > 0
                select g.Key
            ).ToListAsync();
        }
    }
}