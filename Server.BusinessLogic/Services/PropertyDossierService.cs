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
        private readonly IPropertyDossierRepository _propertyDossierRepository;

        public PropertyDossierService(IAuthLibraryService authLibraryService, IFileService fileService, IAccountRepository accountRepository, IModuleRepository moduleRepository, IPropertyDossierRepository propertyDossierRepository)
        {
            _authLibraryService = authLibraryService;
            _fileService = fileService;
            _accountRepository = accountRepository;
            _moduleRepository = moduleRepository;
            _propertyDossierRepository = propertyDossierRepository;
        }

        public async Task NewPropertyDossier(string accessToken, PropertyDossierRequest propertyDossier)
        {
            try
            {
                Account account = await _authLibraryService.FetchAccount(accessToken);
                Module module = await _moduleRepository.GetModuleByModuleName(Common.Enums.Module.PropertyDossier);
                AccountModule accountModule = await _accountRepository.CheckAccountModule(module, account, Common.Enums.Role.User, Common.Enums.Permission.CreatePropertyDossier);
                Document document = await _propertyDossierRepository.CreateNewDoc(module, account);
                await _fileService.UploadFileAsync(propertyDossier.Files, account, document);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}