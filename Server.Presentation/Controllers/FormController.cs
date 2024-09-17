using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Server.BusinessLogic.Interfaces;
using Server.Common.DTOs;
using Server.Common.Enums;
using Server.Models.DTOs.Form;
using Server.Presentation.CustomAttributes;

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
        [CustomAuthorize(Permission.CanViewAllForms, Permission.CanViewTenantForms, Permission.CanViewOwnForms)]
        public async Task<IActionResult> GetAllRequest([FromQuery] string? status, string? label, string? type)
        {
            if (!Enum.TryParse(HttpContext.Items["UserPermission"]?.ToString(), out Permission userPermission))
            {
                return Forbid();
            }

            try
            {
                var authorizationHeader = Request.Headers[HeaderNames.Authorization];
                if (authorizationHeader.ToString().StartsWith("Bearer"))
                {
                    var accessToken = authorizationHeader.ToString()["Bearer ".Length..].Trim();
                    List<FormResponse> formResponses = await _formService.HandleGetAllFormRequest(accessToken, userPermission, status, label, type);

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
        [CustomAuthorize(Permission.UpdateForm)]
        public async Task<IActionResult> UpdateRequest([FromQuery] long id, [FromBody] FormUpdate request)
        {
            try
            {
                await _formService.HandleAdminResponse(id, request.Response, request.Status);
                return Ok();
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}