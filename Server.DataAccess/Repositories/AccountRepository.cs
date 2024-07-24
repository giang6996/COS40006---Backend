using Microsoft.EntityFrameworkCore;
using Server.Common.Models;
using Server.DataAccess.Interfaces;

namespace Server.DataAccess.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _db;

        public AccountRepository(AppDbContext db)
        {
            _db = db;
        }

        public bool CheckAccountExist(string email)
        {
            return _db.Accounts.Any(u => u.Email == email);
        }

        public async Task AddAccountAsync(Account account)
        {
            _db.Accounts.Add(account);
            await _db.SaveChangesAsync();
        }

        public async Task<Account?> GetAccountByEmailAsync(string email)
        {
            return await _db.Accounts.Where(a => a.Email == email).FirstOrDefaultAsync();
        }
    }
}