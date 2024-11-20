using Server.BusinessLogic.Interfaces;
using Server.Common.Models;
using Server.DataAccess.Interfaces;
using Server.Models.ResponseModels;
using Server.Models.DTOs.Account;
using Server.Common.Enums;
using System.Data;
using Server.Models.DTOs.PropertyDossier;
using Microsoft.AspNetCore.Http;
using Server.Models.DTOs.Document;
using Microsoft.AspNetCore.Identity.Data;
using BCrypt.Net;
using RegisterRequest = Server.Models.DTOs.Account.RegisterRequest;
using LoginRequest = Server.Models.DTOs.Account.LoginRequest;

namespace Server.BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAuthLibraryService _authLibraryService;
        private readonly IAccountRepository _accountRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly IResidentRepository _residentRepository;
        private readonly IAuthorizeRepository _authorizeRepository;
        private readonly IPropertyDossierService _propertyDossierService;

        public AccountService(IAuthLibraryService authLibraryService, IAccountRepository accountRepository, ITokenRepository tokenRepository, IAuthorizeRepository authorizeRepository, IPropertyDossierService propertyDossierService, IResidentRepository residentRepository)
        {
            _authLibraryService = authLibraryService;
            _accountRepository = accountRepository;
            _tokenRepository = tokenRepository;
            _authorizeRepository = authorizeRepository;
            _propertyDossierService = propertyDossierService;
            _residentRepository = residentRepository;
        }

        public async Task<Token> RegisterAsync(RegisterRequest request, List<IFormFile> documents)
        {
            if (_accountRepository.CheckAccountExist(request.Email))
                throw new InvalidOperationException("This email already exists!");

            string passwordHashed = BCrypt.Net.BCrypt.HashPassword(request.Password);

            Account account = new()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Password = passwordHashed,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                TenantId = 1, // FOR TESTING ONLY!!!
                Status =  AccountStatus.Pending.ToString()
            };

            await _accountRepository.AddAccountAsync(account);
            await _authorizeRepository.AssignAccountRole(account, Common.Enums.Role.User); // assign account as Resident as default

            ApartmentInfoRequest apartmentInfo = new()
            {
                BuildingId = request.BuildingId,
                ApartmentId = request.ApartmentId
            };

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

                await _propertyDossierService.NewPropertyDossier(at, documents, apartmentInfo);

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

            if (account.Status == "Pending")
                throw new Exception("Account not active");

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

            var roles = account.AccountRoles.Select(ar => ar.Role.Name).ToList();
            var firstDocument = account.Documents.FirstOrDefault();
            var apartmentRoomNumber = firstDocument?.Apartment?.RoomNumber;
            var buildingName = firstDocument?.Building?.BuildingName;
            var buildingAddress = firstDocument?.Building?.BuildingAddress;
            AccountDTO accountDTO = new()
            {
                Id = account.Id,
                TenantId = account.TenantId,
                FirstName = account.FirstName,
                LastName = account.LastName,
                Email = account.Email,
                PhoneNumber = account.PhoneNumber,
                Status = account.Status,
                Roles = roles,
                Documents = account.Documents
                    .SelectMany(d => d.DocumentDetails)
                    .Select(dd => new DocumentDetails
                    {
                        Id = dd.Id,
                        Name = dd.Name,
                        DocumentDesc = dd.DocumentDesc,
                        Status = dd.Status,
                        DocumentLink = dd.DocumentLink
                    }).ToList(),

                // Map apartment and building details
                Apartment = apartmentRoomNumber?.ToString(),
                Building = buildingName + ' ' + buildingAddress,
            };

            return accountDTO;
        }

        public async Task<AccountDTO> GetAccountByIdAsync(long accountId)
        {
            // Fetch the account by its ID
            Account account = await _accountRepository.GetAccountByAccountIdAsync(accountId)
                ?? throw new Exception("Account not found");

            var roles = account.AccountRoles.Select(ar => ar.Role.Name).ToList();

            // Get apartment and building info from the first document, if available
            var firstDocument = account.Documents.FirstOrDefault();
            var apartmentRoomNumber = firstDocument?.Apartment?.RoomNumber;
            var buildingName = firstDocument?.Building?.BuildingName;
            var buildingAddress = firstDocument?.Building?.BuildingAddress;

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
                Roles = roles,
                Documents = account.Documents
                    .SelectMany(d => d.DocumentDetails)
                    .Select(dd => new DocumentDetails
                    {
                        Id = dd.Id,
                        Name = dd.Name,
                        DocumentDesc = dd.DocumentDesc,
                        Status = dd.Status,
                        DocumentLink = dd.DocumentLink
                    }).ToList(), 

                // Map apartment and building details
                Apartment = apartmentRoomNumber?.ToString(),
                Building = buildingName + ' ' + buildingAddress,
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
                Roles = account.AccountRoles.Select(ar => ar.Role.Name).ToList(),  // Fetch roles
                Apartment = account.Documents.FirstOrDefault()?.Apartment?.RoomNumber.ToString(), // Retrieve Apartment info if linked
                Building = account.Documents.FirstOrDefault()?.Building?.BuildingName    // Retrieve Building info if linked
            }).ToList();

            return accountDTOs;
        }

        public async Task UpdatePasswordAsync(string accessToken, UpdatePasswordRequest request)
        {
            // Fetch the account from the access token
            Account account = await _tokenRepository.FetchAccountFromDb(accessToken);
            if (account == null) throw new Exception("Account not found");

            // Verify the old password
            if (!BCrypt.Net.BCrypt.Verify(request.OldPassword, account.Password))
            {
                throw new Exception("Incorrect old password.");
            }

            // Ensure new password and confirmation match
            if (request.NewPassword != request.ConfirmPassword)
            {
                throw new Exception("New password and confirmation do not match.");
            }

            // Update and hash the new password
            account.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            await _residentRepository.UpdateAsync(account);
        }


    }
}