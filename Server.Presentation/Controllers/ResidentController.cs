using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Server.BusinessLogic.Interfaces;
using Server.BusinessLogic.Services;
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

        [Authorize]
        [HttpGet("download-document/{documentLink}")]
        public IActionResult DownloadDocument(string documentLink)
        {
            // Decode the document link to handle any encoded characters
            string decodedDocumentLink = Uri.UnescapeDataString(documentLink);

            // Construct the full file path
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles", decodedDocumentLink);

            // Ensure the path uses the correct directory separators for Windows
            filePath = filePath.Replace('\\', Path.DirectorySeparatorChar).Replace('/', Path.DirectorySeparatorChar);

            // Check if the file exists
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Document not found");
            }

            // Read the file content and return it as a downloadable response
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            string fileName = Path.GetFileName(filePath);
            return File(fileBytes, "application/octet-stream", fileName);
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

        [HttpDelete("delete-account")]
        [CustomAuthorize(Common.Enums.Permission.DeleteAccount)]
        public async Task<IActionResult> DeleteAccount([FromBody] DeleteResidentRequest request)
        {
            try
            {
                await _residentService.DeleteAccountAsync(request.AccountId);
                return Ok("Account and all related records deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequest request)
        {
            try
            {
                string accessToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                await _residentService.UpdateProfileAsync(accessToken, request);
                return Ok("Profile updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to update profile: {ex.Message}");
            }
        }
    }
}