using Server.Models.DTOs;
using Server.Models.ResponseModels;

namespace Server.BusinessLogic.Interfaces
{
    public interface IAccountService
    {
        Task<Token> RegisterAsync(Account account);
    }
}