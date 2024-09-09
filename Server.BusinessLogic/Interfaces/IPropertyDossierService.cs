using Microsoft.AspNetCore.Http;
using Server.Models.DTOs.PropertyDossier;

namespace Server.BusinessLogic.Interfaces
{
    public interface IPropertyDossierService
    {
        Task NewPropertyDossier(string accessToken, List<IFormFile> files, ApartmentInfoRequest apartmentInfo);
    }
}