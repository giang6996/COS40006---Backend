using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.BusinessLogic.Interfaces;

namespace Server.Presentation.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [Authorize]
    public class TokenController : ControllerBase
    {
        private readonly IAuthLibrary _authLibrary;

        public TokenController(IAuthLibrary authLibrary)
        {
            _authLibrary = authLibrary;
        }

        [AllowAnonymous]
        [HttpPost("new-token")]
        public async Task<IActionResult> RefreshToken()
        {
            // Get the refresh token from the header
            string? refreshToken = Request.Headers["refreshToken"];
            // Check if the refresh token is null or empty
            if (string.IsNullOrEmpty(refreshToken))
            {
                return BadRequest("Refresh token is required");
            }
            // Get the access token from the authorization header
            string? accessToken = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            // Check if the access token is null or empty
            if (string.IsNullOrEmpty(accessToken))
            {
                return BadRequest("Access token is required");
            }

            try
            {
                var token = await _authLibrary.GenerateNewToken(accessToken, refreshToken);

                Response.Cookies.Append("refreshToken", token.RefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.Now.AddDays(7)
                });

                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}