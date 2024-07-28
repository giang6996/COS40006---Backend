using Server.Common.Models;

namespace Server.DataAccess.Interfaces
{
    public interface IAccountRepository
    {
        bool CheckAccountExist(string email);
        Task AddAccountAsync(Account account);
        Task<Account?> GetAccountByEmailAsync(string email);
        Task<Account> GetAccountByAccountIdAsync(long accountId);
        Task<AccountModule> CheckAccountModule(Module module, Account account, Server.Common.Enums.Role roleName, Server.Common.Enums.Permission permissionName);
        Task UpdateAccountStatus(Account account, Server.Common.Enums.AccountStatus accountStatus);
    }
}