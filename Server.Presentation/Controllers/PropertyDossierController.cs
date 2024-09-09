using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Server.BusinessLogic.Interfaces;
using Server.Models.DTOs.PropertyDossier;

namespace Server.Presentation.Controllers
{
    [ApiController]
    [Route("api/property-dossier")]
    [Authorize]
    public class PropertyDossier : ControllerBase
    {
        private readonly IPropertyDossierService _propertyDossierService;

        public PropertyDossier(IPropertyDossierService propertyDossierService)
        {
            _propertyDossierService = propertyDossierService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadPropertyDossier([FromForm] List<IFormFile> files, [FromForm] ApartmentInfoRequest apartmentInfo)
        {
            try
            {
                var authorizationHeader = Request.Headers[HeaderNames.Authorization];
                if (authorizationHeader.ToString().StartsWith("Bearer"))
                {
                    var accessToken = authorizationHeader.ToString()["Bearer ".Length..].Trim();

                    await _propertyDossierService.NewPropertyDossier(accessToken, files, apartmentInfo);
                    return Ok("Uploaded successfully");
                }

                throw new Exception("Unexpected Error");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}