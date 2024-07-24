using Server.BusinessLogic.Interfaces;
using Server.Common.Models;
using Server.DataAccess.Interfaces;
using Server.Models.ResponseModels;

namespace Server.BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAuthLibrary _authLibrary;
        private readonly IAccountRepository _accountRepository;
        private readonly ITokenService _tokenService;

        public AccountService(IAuthLibrary authLibrary, IAccountRepository accountRepository, ITokenService tokenService)
        {
            _authLibrary = authLibrary;
            _accountRepository = accountRepository;
            _tokenService = tokenService;
        }

        public async Task<Token> RegisterAsync(Server.Models.DTOs.Account account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            if (_accountRepository.CheckAccountExist(account.Email))
                throw new InvalidOperationException("This email already exists!");

            string passwordHashed = BCrypt.Net.BCrypt.HashPassword(account.Password);

            Server.Common.Models.Account accountToDb = new()
            {
                TenantId = 0,
                FirstName = account.FirstName,
                LastName = account.LastName,
                Password = passwordHashed,
                Email = account.Email,
            };

            await _accountRepository.AddAccountAsync(accountToDb);

            var accountFromDb = await _accountRepository.GetAccountByEmailAsync(account.Email);
            var token = _authLibrary.Generate(accountFromDb);

            if (token is (string at, string rt))
            {
                (string accessTokenValue, string refreshTokenValue) = (at, rt);
                RefreshToken refreshToken = new()
                {
                    Value = refreshTokenValue,
                    AccountId = accountFromDb.Id,
                    ExpirationDate = DateTime.Now.AddDays(7),
                    Revoked = false,
                };
                await _tokenService.AddRefreshTokenAsync(refreshToken);

                var accessToken = new AccessToken()
                {
                    Value = accessTokenValue,
                    RtId = refreshToken.Id,
                    ExpirationDate = DateTime.Now.AddDays(2),
                    Revoked = false,
                };
                await _tokenService.AddAccessTokenAsync(accessToken);

                Token returnToken = new()
                {
                    AccessToken = accessTokenValue.ToString(),
                    RefreshToken = refreshTokenValue.ToString(),
                };

                return returnToken;
            }

            throw new InvalidOperationException("Failed to generate tokens.");
        }
    }
}