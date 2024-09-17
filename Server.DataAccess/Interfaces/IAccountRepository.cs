using Server.Common.Models;

namespace Server.DataAccess.Interfaces
{
    public interface IAccountRepository
    {
        long CountAccount();
        bool CheckAccountExist(string email);
        Task AddAccountAsync(Account account);
        Task<Account?> GetAccountByEmailAsync(string email);
        Task<Account> GetAccountByAccountIdAsync(long accountId);
        Task UpdateAccountStatus(Account account, Server.Common.Enums.AccountStatus accountStatus);
    }
}