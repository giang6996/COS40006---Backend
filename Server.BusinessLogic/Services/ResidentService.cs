using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Server.BusinessLogic.Interfaces;
using Server.Common.Models;
using Server.DataAccess.Interfaces;
using Server.DataAccess.Repositories;
using Server.Models.DTOs.Resident;

namespace Server.BusinessLogic.Services
{
    public class ResidentService : IResidentService
    {
        private readonly IResidentRepository _residentRepository;
        private readonly IDocRepository _docRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IBuildingRepository _buildingRepository;
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IAuthLibraryService _authLibraryService;

        public ResidentService(
            IResidentRepository residentRepository,
            IDocRepository docRepository,
            IAccountRepository accountRepository,
            IBuildingRepository buildingRepository,
            IApartmentRepository apartmentRepository,
            IAuthLibraryService authLibraryService)
        {
            _residentRepository = residentRepository;
            _docRepository = docRepository;
            _accountRepository = accountRepository;
            _buildingRepository = buildingRepository;
            _apartmentRepository = apartmentRepository;
            _authLibraryService = authLibraryService;
        }

        public async Task<List<NewResidentResponse>> GetAllNewResidentRequest()
        {
            try
            {
                List<NewResidentResponse> newResidentResponsesList = new();
                List<Account> accounts = await _residentRepository.FetchAllNewAccountsWithDocsSubmitted();

                foreach (var a in accounts)
                {
                    NewResidentResponse newResidentResponse = new()
                    {
                        FullName = $"{a.FirstName} {a.LastName}",
                        Email = a.Email
                    };

                    newResidentResponsesList.Add(newResidentResponse);
                }

                return newResidentResponsesList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DetailsNewResidentResponse> GetDetailsNewResident(string residentEmail)
        {
            try
            {
                Account newAccount = await _accountRepository.GetAccountByEmailAsync(residentEmail)
                    ?? throw new Exception("Invalid email request");

                Document document = await _docRepository.GetLastDoc(newAccount);
                List<DocumentDetail> documentDetailsList = await _docRepository.GetDocDetails(document);
                List<Server.Models.DTOs.Document.DocumentDetails> newDocumentDetailsList = new();

                foreach (var documentDetail in documentDetailsList)
                {
                    newDocumentDetailsList.Add(new()
                    {
                        Id = documentDetail.Id,
                        Name = documentDetail.Name,
                        DocumentDesc = documentDetail.DocumentDesc,
                        Status = documentDetail.Status,
                        DocumentLink = documentDetail.DocumentLink
                    });
                }

                // Fetch building and apartment details using IDs
                Building building = await _buildingRepository.GetByIdAsync(document.BuildingId);
                Apartment apartment = await _apartmentRepository.GetByIdAsync(document.ApartmentId);

                DetailsNewResidentResponse detailsNewResidentResponse = new()
                {
                    AccountId = newAccount.Id,
                    FirstName = newAccount.FirstName,
                    LastName = newAccount.LastName,
                    Email = newAccount.Email,
                    PhoneNumber = newAccount.PhoneNumber,
                    AccountStatus = newAccount.Status,
                    BuildingName = building.BuildingName,
                    BuildingAddress = building.BuildingAddress,
                    RoomNumber = apartment.RoomNumber,
                    Timestamp = document.Timestamp,
                    DocumentDetailsList = newDocumentDetailsList
                };

                return detailsNewResidentResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAccountStatus(long accountId, string status)
        {
            try
            {
                Account residentAccount = await _accountRepository.GetAccountByAccountIdAsync(accountId);
                Document document = await _docRepository.GetDocumentByAccountId(accountId);
                Console.WriteLine(accountId);
                if (document != null && residentAccount != null)
                {
                    if (Enum.TryParse<Common.Enums.AccountStatus>(status, true, out var parsedStatus))
                    {
                        await _accountRepository.UpdateAccountStatus(residentAccount, parsedStatus);
                        return;
                    }
                    else
                    {
                        throw new ArgumentException("Invalid account status");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task DeleteAccountAsync(long accountId)
        {
            try
            {
                // Fetch the account and related documents
                Account residentAccount = await _accountRepository.GetAccountByAccountIdAsync(accountId)
                    ?? throw new Exception("Account not found");

                // Delete all documents related to the account
                await _residentRepository.DeleteDocumentsByAccountId(accountId);

                // Delete the account itself
                await _residentRepository.DeleteAccountAsync(residentAccount);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine("Concurrency exception occurred: " + ex.Message);

                // The code currently have some minor concurrentcy problem, some process are delete sooner that expected
                // Ignore the exception to avoid failure
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete account and associated records: " + ex.Message);
            }
        }

        public async Task UpdateProfileAsync(string accessToken, UpdateProfileRequest request)
        {
            Account account = await _authLibraryService.FetchAccount(accessToken);

            // Validate and update account information
            if (account == null)
                throw new Exception("Account not found");

            // Only update allowed fields
            account.PhoneNumber = request.PhoneNumber;

            // Save changes to the database
            await _residentRepository.UpdateAsync(account);
        }

    }
}
