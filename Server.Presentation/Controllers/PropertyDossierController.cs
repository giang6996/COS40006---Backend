using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Server.BusinessLogic.Interfaces;
using Server.Common.Models;
using Server.Models.DTOs.PropertyDossier;

namespace Server.Presentation.Controllers
{
    [ApiController]
    [Route("api/property-dossier")]
    [Authorize]
    public class PropertyDossier : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly IAuthLibrary _authLibrary;

        public PropertyDossier(IFileService fileService, IAuthLibrary authLibrary)
        {
            _fileService = fileService;
            _authLibrary = authLibrary;
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
                    Account account = await _authLibrary.FetchAccount(accessToken);
                    await _fileService.UploadFileAsync(request.Files, account);

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