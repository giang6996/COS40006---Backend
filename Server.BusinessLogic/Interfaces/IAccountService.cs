using Server.Models.DTOs.Account;
using Server.Models.ResponseModels;

namespace Server.BusinessLogic.Interfaces
{
    public interface IAccountService
    {
        Task<Token> RegisterAsync(RegisterRequest request);
        Task<Token> LoginAsync(LoginRequest request);
    }
}