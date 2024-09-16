using Microsoft.EntityFrameworkCore;
using Server.Common.Models;
using Server.DataAccess.Interfaces;

namespace Server.DataAccess.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _db;
        private readonly IAuthorizeRepository _authorizeRepository;

        public AccountRepository(AppDbContext db, IAuthorizeRepository authorizeRepository)
        {
            _db = db;
            _authorizeRepository = authorizeRepository;
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

        public async Task<AccountModule> CheckAccountModule(Module module, Account account, Server.Common.Enums.Role roleName, Server.Common.Enums.Permission permissionName)
        {
            try
            {
                AccountModule? accountModule = await _db.AccountModules.Where(am => am.AccountId == account.Id && am.ModuleId == module.Id).FirstOrDefaultAsync();

                if (accountModule != null)
                {
                    return accountModule;
                }

                throw new Exception("Can not access this module");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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