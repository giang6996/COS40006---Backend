using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Server.BusinessLogic.Interfaces;
using Server.Models.DTOs.Resident;

namespace Server.Presentation.Controllers
{
    [ApiController]
    [Route("api/resident")]
    [Authorize]
    public class ResidentController : ControllerBase
    {
        private readonly IResidentService _residentService;

        public ResidentController(IResidentService residentService)
        {
            _residentService = residentService;
        }

        [HttpGet("get-pending-accounts/all")]
        public async Task<IActionResult> GetAllNewResidentRequest()
        {
            try
            {
                var authorizationHeader = Request.Headers[HeaderNames.Authorization];
                if (authorizationHeader.ToString().StartsWith("Bearer"))
                {
                    var accessToken = authorizationHeader.ToString().Substring("Bearer ".Length).Trim();
                    List<NewResidentResponse> newResidentResponsesList = await _residentService.GetAllNewResidentRequest(accessToken);
                    return Ok(newResidentResponsesList);
                }

                throw new Exception("Unexpected Error");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-pending-accounts")]
        public async Task<IActionResult> GetDetailsNewResidentRequest([FromQuery] string email)
        {
            try
            {
                var authorizationHeader = Request.Headers[HeaderNames.Authorization];
                if (authorizationHeader.ToString().StartsWith("Bearer"))
                {
                    var accessToken = authorizationHeader.ToString().Substring("Bearer ".Length).Trim();
                    DetailsNewResidentResponse response = await _residentService.GetDetailsNewResident(accessToken, email);

                    return Ok(response);
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