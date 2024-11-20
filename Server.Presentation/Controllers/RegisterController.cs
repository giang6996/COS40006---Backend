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
        private readonly ICookieHelper _cookieHelper;

        public RegisterController(IAccountService accountService, ICookieHelper cookieHelper)
        {
            _accountService = accountService;
            _cookieHelper = cookieHelper;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterRequest request, [FromForm] List<IFormFile> documents)
        {
            if (request == null)
            {
                return BadRequest(new { message = "Invalid register request." });
            }

            try
            {
                // Pass the `request` object along with `documents` to `RegisterAsync`
                var token = await _accountService.RegisterAsync(request, documents);

                _cookieHelper.SetCookie(Response, "refreshToken", token.RefreshToken, new CookieOptions
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
            catch (Exception)
            {
                // Return a custom error response for unhandled exceptions
                return StatusCode(500, new { message = "An error occurred while processing your request. Please try again later." });
            }
        }
    }
}

