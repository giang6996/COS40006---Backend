using Server.Models.DTOs.Account;
using Server.Models.ResponseModels;

namespace Server.BusinessLogic.Interfaces
{
    public interface IAccountService
    {
        Task<Token> RegisterAsync(RegisterRequest request);
        Task<Token> LoginAsync(LoginRequest request);
        Task<AccountDTO> GetAccountInfos(string accessToken);
        Task<AccountDTO> GetAccountByIdAsync(long accountId);
        Task<List<AccountDTO>> GetAllAccountsAsync();
    }
}