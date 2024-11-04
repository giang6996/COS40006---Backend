using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.BusinessLogic.Interfaces;
using Server.Models.DTOs.Account;

namespace Server.Presentation.Controllers
{
    [ApiController]
    [Route("api/auth/[controller]")]
    [AllowAnonymous]
    public class RegisterController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public RegisterController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (request == null)
            {
                return BadRequest(new { message = "Invalid register request." });
            }

            try
            {
                var token = await _accountService.RegisterAsync(request);

                Response.Cookies.Append("refreshToken", token.RefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.Now.AddDays(7)
                });

                return Ok(token);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register-resident")]
        public async Task<IActionResult> RegisterResident([FromForm] ResidentRegistrationDto registrationDto)
        {
            if (registrationDto == null)
            {
                return BadRequest(new { message = "Invalid resident registration request." });
            }

            try
            {
                await _accountService.RegisterResidentWithDocumentsAsync(registrationDto);
                return Ok(new { message = "Registration submitted successfully. Awaiting admin approval." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred during registration.", details = ex.Message });
            }
        }
    }
}

