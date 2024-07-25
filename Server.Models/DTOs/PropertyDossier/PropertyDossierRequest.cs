using Microsoft.AspNetCore.Http;

namespace Server.Models.DTOs.PropertyDossier
{
    public class PropertyDossierRequest
    {
        public required long ApartmentId { get; set; }
        public required List<IFormFile> Files { get; set; }
    }
}