using Server.BusinessLogic.Interfaces;
using Server.Common.Models;
using Server.DataAccess.Interfaces;
using Server.Models.ResponseModels;
using Server.Models.DTOs.Account;

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

        public async Task<Token> RegisterAsync(RegisterRequest request)
        {
            if (_accountRepository.CheckAccountExist(request.Email))
                throw new InvalidOperationException("This email already exists!");

            string passwordHashed = BCrypt.Net.BCrypt.HashPassword(request.Password);

            Account account = new()
            {
                TenantId = 0,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Password = passwordHashed,
                Email = request.Email,
            };

            await _accountRepository.AddAccountAsync(account);

            var token = _authLibrary.Generate(account);
            if (token is (string at, string rt))
            {
                RefreshToken refreshToken = new()
                {
                    Value = rt,
                    AccountId = account.Id,
                    ExpirationDate = DateTime.Now.AddDays(7),
                    Revoked = false,
                };
                await _tokenService.AddRefreshTokenAsync(refreshToken);

                var accessToken = new AccessToken()
                {
                    Value = at,
                    RtId = refreshToken.Id,
                    ExpirationDate = DateTime.Now.AddMinutes(10),
                    Revoked = false,
                };
                await _tokenService.AddAccessTokenAsync(accessToken);

                Token returnToken = new()
                {
                    AccessToken = at,
                    RefreshToken = rt,
                };

                return returnToken;
            }

            throw new InvalidOperationException("Failed to generate tokens.");
        }

        public async Task<Token> LoginAsync(LoginRequest request)
        {
            Account? account = await _accountRepository.GetAccountByEmailAsync(request.Email) ?? throw new Exception("Account not found");

            if (!BCrypt.Net.BCrypt.Verify(request.Password, account.Password))
                throw new Exception("Incorrect Password");

            var token = _authLibrary.Generate(account);
            if (token is (string at, string rt))
            {
                RefreshToken refreshToken = new()
                {
                    Value = rt,
                    AccountId = account.Id,
                    ExpirationDate = DateTime.Now.AddDays(7),
                    Revoked = false,
                };
                await _tokenService.AddRefreshTokenAsync(refreshToken);

                AccessToken accessToken = new()
                {
                    Value = at,
                    RtId = refreshToken.Id,
                    ExpirationDate = DateTime.Now.AddDays(2),
                    Revoked = false,
                };
                await _tokenService.AddAccessTokenAsync(accessToken);

                Token returnToken = new()
                {
                    AccessToken = at,
                    RefreshToken = rt,
                };

                return returnToken;
            }

            throw new InvalidOperationException("Failed to generate tokens.");
        }
    }
}