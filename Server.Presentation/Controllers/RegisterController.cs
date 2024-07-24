using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.BusinessLogic.Services;
using Server.Common;
using Server.DataAccess.Repositories;

namespace Server.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        private readonly IAuthLibrary _authLibrary;
        private readonly IAccountRepository _accountRepository;
        private readonly ITokenService _tokenService;

        public RegisterController(IAuthLibrary authLibrary, IAccountRepository accountRepository, ITokenService tokenService)
        {
            _authLibrary = authLibrary;
            _accountRepository = accountRepository;
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Account account)
        {
            // Validate user
            if (account == null)
            {
                return BadRequest();
            }

            // Check if the account exists
            if (_accountRepository.CheckAccountExist(account.Email))
            {
                return BadRequest("This email already exists!");
            }

            string passwordHashed = BCrypt.Net.BCrypt.HashPassword(account.Password);

            Account accountToDb = new()
            {
                TenantId = 0,
                FirstName = account.FirstName,
                LastName = account.LastName,
                Password = passwordHashed,
                Email = account.Email,
            };

            await _accountRepository.AddAccountAsync(accountToDb);

            // Retrieve the user from database
            Account accountFromDb = await _accountRepository.GetAccountByEmailAsync(account.Email);

            var token = _authLibrary.Generate(accountFromDb);
            if (token is (string at, string rt))
            {
                (string accessTokenValue, string refreshTokenValue) = (at, rt);
                RefreshToken refreshToken = new()
                {
                    Value = refreshTokenValue,
                    AccountId = accountFromDb.Id,
                    ExpirationDate = DateTime.Now.AddDays(7),
                    Revoked = false,
                };


                await _tokenService.AddRefreshTokenAsync(refreshToken);

                var accessToken = new AccessToken()
                {
                    Value = accessTokenValue,
                    RtId = refreshToken.Id,
                    ExpirationDate = DateTime.Now.AddMinutes(2),
                    Revoked = false,
                };
                await _tokenService.AddAccessTokenAsync(accessToken);

                // Assign refresh token for cookies
                Response.Cookies.Append("refreshToken", refreshTokenValue, new CookieOptions()
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.Now.AddDays(7)
                });

                ReturnToken returnToken = new()
                {
                    AccessToken = accessTokenValue.ToString(),
                    RefreshToken = refreshTokenValue.ToString(),
                };

                return Ok(returnToken);
            }

            return BadRequest();
        }
    }

    public class ReturnToken
    {
        [Required]
        public required string AccessToken { get; set; }
        [Required]
        public required string RefreshToken { get; set; }
    }
}

