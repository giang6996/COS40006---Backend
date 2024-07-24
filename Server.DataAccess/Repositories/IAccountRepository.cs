using Server.Common;

namespace Server.DataAccess.Repositories
{
    public interface IAccountRepository
    {
        bool CheckAccountExist(string email);
        Task AddAccountAsync(Account account);
        Task<Account> GetAccountByEmailAsync(string email);
    }
}