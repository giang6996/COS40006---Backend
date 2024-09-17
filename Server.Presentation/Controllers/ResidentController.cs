using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.BusinessLogic.Interfaces;
using Server.Models.DTOs.Resident;
using Server.Presentation.CustomAttributes;

namespace Server.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ResidentController : ControllerBase
    {
        private readonly IResidentService _residentService;

        public ResidentController(IResidentService residentService)
        {
            _residentService = residentService;
        }

        [HttpGet("get-pending-accounts/all")]
        [CustomAuthorize(Common.Enums.Permission.ReadAllNewResidentRequest)]
        public async Task<IActionResult> GetAllNewResidentRequest()
        {
            try
            {
                List<NewResidentResponse> newResidentResponsesList = await _residentService.GetAllNewResidentRequest();
                return Ok(newResidentResponsesList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-pending-accounts")]
        [CustomAuthorize(Common.Enums.Permission.ReadAllNewResidentRequest)]
        public async Task<IActionResult> GetDetailsNewResidentRequest([FromQuery] string email)
        {
            try
            {
                DetailsNewResidentResponse response = await _residentService.GetDetailsNewResident(email);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("update-account-status")]
        [CustomAuthorize(Common.Enums.Permission.UpdateNewResidentRequest)]
        public async Task<IActionResult> UpdateAccountStatus([FromBody] UpdateAccountStatusRequest request)
        {
            try
            {
                await _residentService.UpdateAccountStatus(request.AccountId, request.Status);
                return Ok("Account Status update successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}