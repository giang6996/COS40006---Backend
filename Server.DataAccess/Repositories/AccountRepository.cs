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

        public async Task<Account> GetAccountByAccountIdAsync(long accountId)
        {
            return await _db.Accounts.Where(a => a.Id == accountId).FirstOrDefaultAsync() ?? throw new Exception("Account not found");
        }

        public async Task UpdateAccountStatus(Account account, Common.Enums.AccountStatus accountStatus)
        {
            account.Status = accountStatus.ToString();
            await _db.SaveChangesAsync();
        }

        public long CountAccount()
        {
            return _db.Accounts.Count();
        }
    }
}