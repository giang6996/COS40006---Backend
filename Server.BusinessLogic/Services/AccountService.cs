using Server.BusinessLogic.Interfaces;
using Server.Common.Models;
using Server.DataAccess.Interfaces;
using Server.Models.ResponseModels;
using Server.Models.DTOs.Account;
using Server.Common.Enums;
using System.Data;

namespace Server.BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAuthLibraryService _authLibraryService;
        private readonly IAccountRepository _accountRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly IAuthorizeRepository _authorizeRepository;

        public AccountService(IAuthLibraryService authLibraryService, IAccountRepository accountRepository, ITokenRepository tokenRepository, IAuthorizeRepository authorizeRepository)
        {
            _authLibraryService = authLibraryService;
            _accountRepository = accountRepository;
            _tokenRepository = tokenRepository;
            _authorizeRepository = authorizeRepository;
        }

        public async Task<Token> RegisterAsync(RegisterRequest request)
        {
            if (_accountRepository.CheckAccountExist(request.Email))
                throw new InvalidOperationException("This email already exists!");

            string passwordHashed = BCrypt.Net.BCrypt.HashPassword(request.Password);

            long accountCount = _accountRepository.CountAccount();

            Account account = new()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Password = passwordHashed,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                TenantId = 1, // FOR TESTING ONLY!!!
                Status = accountCount == 0 ? AccountStatus.Active.ToString() : AccountStatus.Pending.ToString()
            };

            await _accountRepository.AddAccountAsync(account);
            await _authorizeRepository.AssignAccountRole(account, accountCount == 0 ? Server.Common.Enums.Role.Admin : Common.Enums.Role.User);

            var token = _authLibraryService.Generate(account);
            if (token is (string at, string rt))
            {
                RefreshToken refreshToken = new()
                {
                    Value = rt,
                    AccountId = account.Id,
                    ExpirationDate = DateTime.Now.AddDays(7),
                    Revoked = false,
                };
                await _tokenRepository.AddRefreshTokenAsync(refreshToken);

                var accessToken = new AccessToken()
                {
                    Value = at,
                    RtId = refreshToken.Id,
                    ExpirationDate = DateTime.Now.AddDays(2),
                    Revoked = false,
                };
                await _tokenRepository.AddAccessTokenAsync(accessToken);

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

            var token = _authLibraryService.Generate(account);
            if (token is (string at, string rt))
            {
                RefreshToken refreshToken = new()
                {
                    Value = rt,
                    AccountId = account.Id,
                    ExpirationDate = DateTime.Now.AddDays(7),
                    Revoked = false,
                };
                await _tokenRepository.AddRefreshTokenAsync(refreshToken);

                AccessToken accessToken = new()
                {
                    Value = at,
                    RtId = refreshToken.Id,
                    ExpirationDate = DateTime.Now.AddDays(2),
                    Revoked = false,
                };
                await _tokenRepository.AddAccessTokenAsync(accessToken);

                Token returnToken = new()
                {
                    AccessToken = at,
                    RefreshToken = rt,
                };

                return returnToken;
            }

            throw new InvalidOperationException("Failed to generate tokens.");
        }

        public async Task<AccountDTO> GetAccountInfos(string accessToken)
        {
            Account account = await _authLibraryService.FetchAccount(accessToken);
            AccountDTO accountDTO = new()
            {
                Id = account.Id,
                TenantId = account.TenantId,
                FirstName = account.FirstName,
                LastName = account.LastName,
                Email = account.Email,
                PhoneNumber = account.PhoneNumber,
                Status = account.Status
            };

            return accountDTO;
        }

        public async Task<AccountDTO> GetAccountByIdAsync(long accountId)
        {
            // Fetch the account by its ID
            Account? account = await _accountRepository.GetAccountByAccountIdAsync(accountId);
            if (account == null)
            {
                throw new Exception("Account not found");
            }

            var roles = account.AccountRoles.Select(ar => ar.Role.Name).ToList();

            // Map the account to AccountDTO
            AccountDTO accountDTO = new()
            {
                Id = account.Id,
                TenantId = account.TenantId,
                FirstName = account.FirstName,
                LastName = account.LastName,
                Email = account.Email,
                PhoneNumber = account.PhoneNumber,
                Status = account.Status,
                Roles = roles // Return the list of roles for this account
            };

            return accountDTO;
        }

        public async Task<List<AccountDTO>> GetAllAccountsAsync()
        {
            var accounts = await _accountRepository.GetAllAccountsAsync();
            var accountDTOs = accounts.Select(account => new AccountDTO
            {
                Id = account.Id,
                FirstName = account.FirstName,
                LastName = account.LastName,
                Email = account.Email,
                PhoneNumber = account.PhoneNumber,
                Status = account.Status,
                Roles = account.AccountRoles.Select(ar => ar.Role.Name).ToList()  // Fetch roles
            }).ToList();

            return accountDTOs;
        }

    }
}