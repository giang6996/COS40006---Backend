using System.Security.Claims;
using Microsoft.AspNetCore.Http;
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
        private readonly IModuleRepository _moduleRepository;
        private readonly IPropertyDossierRepository _propertyDossierRepository;

        public PropertyDossierService(IAuthLibraryService authLibraryService, IFileService fileService, IModuleRepository moduleRepository, IPropertyDossierRepository propertyDossierRepository)
        {
            _authLibraryService = authLibraryService;
            _fileService = fileService;
            _moduleRepository = moduleRepository;
            _propertyDossierRepository = propertyDossierRepository;
        }

        public async Task NewPropertyDossier(string accessToken, List<IFormFile> files, ApartmentInfoRequest apartmentInfo)
        {
            try
            {
                long accountId = (long)Convert.ToDouble(_authLibraryService.GetClaimValue(ClaimTypes.NameIdentifier, accessToken));
                Module module = await _moduleRepository.GetModuleByModuleName(Common.Enums.Module.PropertyDossier);
                Document document = await _propertyDossierRepository.CreateNewDoc(module, accountId, apartmentInfo.RoomNumber, apartmentInfo.BuildingName, apartmentInfo.BuildingAddress);
                await _fileService.UploadFileAsync(files, accountId, document);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}