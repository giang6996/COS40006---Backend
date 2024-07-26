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

        public ResidentService(IAuthLibraryService authLibraryService, IAuthorizeRepository authorizeRepository, IResidentRepository residentRepository)
        {
            _authLibraryService = authLibraryService;
            _authorizeRepository = authorizeRepository;
            _residentRepository = residentRepository;
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
    }
}