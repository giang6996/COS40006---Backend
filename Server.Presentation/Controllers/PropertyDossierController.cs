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
        private readonly IPropertyDossierService _propertyDossier;

        public PropertyDossier(IPropertyDossierService propertyDossier)
        {
            _propertyDossier = propertyDossier;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadPropertyDossier([FromForm] PropertyDossierRequest request)
        {
            try
            {
                var authorizationHeader = Request.Headers[HeaderNames.Authorization];
                if (authorizationHeader.ToString().StartsWith("Bearer"))
                {
                    var accessToken = authorizationHeader.ToString().Substring("Bearer ".Length).Trim();

                    await _propertyDossier.NewPropertyDossier(accessToken, request);
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