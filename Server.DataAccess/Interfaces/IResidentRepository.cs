using Server.Common.Models;

namespace Server.DataAccess.Interfaces
{
    public interface IResidentRepository
    {
        Task<List<Account>> FetchAllNewAccountsWithDocsSubmitted();
        Task DeleteDocumentsByAccountId(long accountId);    
        Task DeleteAccountAsync(Account account);
        Task UpdateAsync(Account account);
    }
}