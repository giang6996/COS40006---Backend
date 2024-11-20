using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Server.BusinessLogic.Interfaces;
using Server.BusinessLogic.Services;
using Server.Models.DTOs.Account;
using Server.Presentation.CustomAttributes;
using System.Security.Claims;

namespace Server.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // Admin can view any user, resident can view only their own profile
        [HttpGet("profile")]
        public async Task<IActionResult> GetAccountProfile([FromQuery] long? accountId)
        {
            try
            {
                var authorizationHeader = Request.Headers[HeaderNames.Authorization];
                if (authorizationHeader.ToString().StartsWith("Bearer"))
                {
                    var accessToken = authorizationHeader.ToString()["Bearer ".Length..].Trim();

                    // Get the role from claims
                    var claimsIdentity = User.Identity as ClaimsIdentity;
                    var roleClaim = claimsIdentity?.FindFirst(ClaimTypes.Role)?.Value;

                    // Admin Role - Can view any user's profile
                    if (roleClaim == "Admin")
                    {
                        if (accountId.HasValue)
                        {
                            // Admin views other user's profile by accountId
                            AccountDTO accountDTO = await _accountService.GetAccountByIdAsync(accountId.Value);
                            return Ok(accountDTO);
                        }
                        else
                        {
                            return BadRequest("Account ID is required for Admin to view another user's profile.");
                        }
                    }

                    // Resident Role - Can view only their own profile
                    AccountDTO residentProfile = await _accountService.GetAccountInfos(accessToken);
                    return Ok(residentProfile);
                }

                throw new Exception("Unexpected Error");
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("list")]
        [Authorize(Roles = "Admin")]  // Only admins can access this
        public async Task<IActionResult> GetAllAccounts()
        {
            try
            {
                // Fetch all accounts via the service layer
                var accounts = await _accountService.GetAllAccountsAsync();
                return Ok(accounts);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] UpdatePasswordRequest request)
        {
            try
            {
                string accessToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                await _accountService.UpdatePasswordAsync(accessToken, request);
                return Ok("Password reset successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to reset password: {ex.Message}");
            }
        }
    }
}
