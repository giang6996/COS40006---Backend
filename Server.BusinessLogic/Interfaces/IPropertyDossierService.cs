using Server.Models.DTOs.PropertyDossier;

namespace Server.BusinessLogic.Interfaces
{
    public interface IPropertyDossierService
    {
        Task NewPropertyDossier(string accessToken, PropertyDossierRequest propertyDossier);
    }
}