using Server.BusinessLogic.Interfaces;
using Server.Common.Models;
using Server.DataAccess.Interfaces;
using Server.Models.DTOs.Resident;

namespace Server.BusinessLogic.Services
{
    public class ResidentService : IResidentService
    {
        private readonly IAuthLibraryService _authLibraryService;
        private readonly IAuthorizeRepository _authorizeRepository;
        private readonly IResidentRepository _residentRepository;
        private readonly IDocRepository _docRepository;
        private readonly IAccountRepository _accountRepository;

        public ResidentService(IAuthLibraryService authLibraryService, IAuthorizeRepository authorizeRepository, IResidentRepository residentRepository, IDocRepository docRepository, IAccountRepository accountRepository)
        {
            _authLibraryService = authLibraryService;
            _authorizeRepository = authorizeRepository;
            _residentRepository = residentRepository;
            _docRepository = docRepository;
            _accountRepository = accountRepository;
        }

        public async Task<List<NewResidentResponse>> GetAllNewResidentRequest(string accessToken)
        {
            try
            {
                Account account = await _authLibraryService.FetchAccount(accessToken);
                if (await _authorizeRepository.VerifyModulePermission(account, Common.Enums.Role.Admin, Common.Enums.Permission.ReadAllNewResidentRequest))
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

                throw new Exception("No permission");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<DetailsNewResidentResponse> GetDetailsNewResident(string accessToken, string residentEmail)
        {
            try
            {
                Account account = await _authLibraryService.FetchAccount(accessToken);
                if (await _authorizeRepository.VerifyModulePermission(account, Common.Enums.Role.Admin, Common.Enums.Permission.ReadAllNewResidentRequest))
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

                throw new Exception("No permission");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}