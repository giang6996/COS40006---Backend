using Microsoft.AspNetCore.Http.HttpResults;
using Server.BusinessLogic.Interfaces;
using Server.Common.Models;
using Server.DataAccess.Interfaces;
using Server.Models.DTOs.Resident;

namespace Server.BusinessLogic.Services
{
    public class ResidentService : IResidentService
    {
        private readonly IResidentRepository _residentRepository;
        private readonly IDocRepository _docRepository;
        private readonly IAccountRepository _accountRepository;

        public ResidentService(IResidentRepository residentRepository, IDocRepository docRepository, IAccountRepository accountRepository)
        {
            _residentRepository = residentRepository;
            _docRepository = docRepository;
            _accountRepository = accountRepository;
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
                        FullName = a.FirstName + a.LastName,
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
                Account newAccount = await _accountRepository.GetAccountByEmailAsync(residentEmail) ?? throw new Exception("Invalid email request");
                Document document = await _docRepository.GetLastDoc(newAccount);
                List<DocumentDetail> documentDetailsList = await _docRepository.GetDocDetails(document);
                List<Server.Models.DTOs.Document.DocumentDetails> newDocumentDetailsList = new();

                foreach (var documentDetails in documentDetailsList)
                {
                    newDocumentDetailsList.Add(new()
                    {
                        Id = documentDetails.Id,
                        Name = documentDetails.Name,
                        DocumentDesc = documentDetails.DocumentDesc,
                        Status = documentDetails.Status,
                        DocumentLink = documentDetails.DocumentLink
                    });
                }

                DetailsNewResidentResponse detailsNewResidentResponse = new()
                {
                    AccountId = newAccount.Id,
                    FirstName = newAccount.FirstName,
                    LastName = newAccount.LastName,
                    Email = newAccount.Email,
                    PhoneNumber = newAccount.PhoneNumber,
                    AccountStatus = newAccount.Status,
                    RoomNumber = document.RoomNumber,
                    BuildingName = document.BuildingName,
                    BuildingAddress = document.BuildingAddress,
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
    }
}