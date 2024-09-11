using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Server.BusinessLogic.Interfaces;
using Server.Common.DTOs;
using Server.Models.DTOs.Form;

namespace Server.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FormController : ControllerBase
    {

        private readonly IFormService _formService;

        public FormController(IFormService formService)
        {
            _formService = formService;
        }

        [HttpPost("new-request")]
        public async Task<IActionResult> NewRequest([FromBody] FormResidentRequest request)
        {
            try
            {
                var authorizationHeader = Request.Headers[HeaderNames.Authorization];
                if (authorizationHeader.ToString().StartsWith("Bearer"))
                {
                    var accessToken = authorizationHeader.ToString()["Bearer ".Length..].Trim();
                    await _formService.HandleNewRequest(request, accessToken);

                    return Ok();
                }

                throw new Exception("Unexpected Error");
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllRequest([FromQuery] string? status, string? label, string? type)
        {
            try
            {
                var authorizationHeader = Request.Headers[HeaderNames.Authorization];
                if (authorizationHeader.ToString().StartsWith("Bearer"))
                {
                    var accessToken = authorizationHeader.ToString()["Bearer ".Length..].Trim();
                    List<FormResponse> formResponses = await _formService.HandleGetAllFormRequest(accessToken, status, label, type);

                    return Ok(formResponses);
                }

                throw new Exception("Unexpected Error");
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("get-detail")]
        public IActionResult GetRequestDetail([FromQuery] long id)
        {
            return Ok(id.ToString());
        }

        [HttpPost("admin-response")]
        public async Task<IActionResult> UpdateRequest([FromQuery] long id, [FromBody] FormUpdate request)
        {
            try
            {
                var authorizationHeader = Request.Headers[HeaderNames.Authorization];
                if (authorizationHeader.ToString().StartsWith("Bearer"))
                {
                    var accessToken = authorizationHeader.ToString()["Bearer ".Length..].Trim();
                    await _formService.HandleAdminResponse(accessToken, id, request.Response, request.Status);
                    return Ok();
                }

                throw new Exception("Unexpected Error");
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}