using Server.BusinessLogic.Interfaces;
using Server.Common.Models;
using Server.DataAccess.Interfaces;
using Server.Models.DTOs.PropertyDossier;

namespace Server.BusinessLogic.Services
{
    public class PropertyDossierService : IPropertyDossierService
    {
        private readonly IAuthLibraryService _authLibraryService;
        private readonly IFileService _fileService;
        private readonly IAccountRepository _accountRepository;
        private readonly IModuleRepository _moduleRepository;

        public PropertyDossierService(IAuthLibraryService authLibraryService, IFileService fileService, IAccountRepository accountRepository, IModuleRepository moduleRepository)
        {
            _authLibraryService = authLibraryService;
            _fileService = fileService;
            _accountRepository = accountRepository;
            _moduleRepository = moduleRepository;
        }

        public async Task NewPropertyDossier(string accessToken, PropertyDossierRequest propertyDossier)
        {
            try
            {
                Account account = await _authLibraryService.FetchAccount(accessToken);
                Module module = await _moduleRepository.GetModuleByModuleName(Common.Enums.Module.PropertyDossier);

                AccountModule accountModule = await _accountRepository.CheckAccountModule(module, account, Common.Enums.Role.User, Common.Enums.Permission.CreatePropertyDossier);
                // await _fileService.UploadFileAsync(propertyDossier.Files, account);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}