using Server.Common.Models;

namespace Server.DataAccess.Interfaces
{
    public interface IResidentRepository
    {
        Task<List<Account>> FetchAllNewAccountsWithDocsSubmitted();
    }
}