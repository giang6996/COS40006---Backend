using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Server.BusinessLogic.Interfaces;
using Server.Models.DTOs.Account;
using Server.Presentation.CustomAttributes;

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

        [HttpGet]
        public async Task<IActionResult> GetAccountInfos()
        {
            try
            {
                var authorizationHeader = Request.Headers[HeaderNames.Authorization];
                if (authorizationHeader.ToString().StartsWith("Bearer"))
                {
                    var accessToken = authorizationHeader.ToString()["Bearer ".Length..].Trim();
                    AccountDTO accountDTO = await _accountService.GetAccountInfos(accessToken);

                    return Ok(accountDTO);
                }

                throw new Exception("Unexpected Error");
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("test")]
        [CustomAuthorize(Common.Enums.Permission.CreatePropertyDossier)]
        public IActionResult Test()
        {
            return Ok("ha ha");
        }
    }
}