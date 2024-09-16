using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Server.BusinessLogic.Interfaces;
using Server.Models.DTOs.Group;
using Server.Presentation.CustomAttributes;

namespace Server.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpPost("create")]
        [CustomAuthorize(Common.Enums.Permission.CreateGroup)]
        public async Task<IActionResult> CreateGroup([FromBody] CreateGroupRequest request)
        {
            try
            {
                var authorizationHeader = Request.Headers[HeaderNames.Authorization];
                if (authorizationHeader.ToString().StartsWith("Bearer"))
                {
                    var accessToken = authorizationHeader.ToString()["Bearer ".Length..].Trim();

                    await _groupService.HandleCreateGroup(accessToken, request);
                    return Ok();
                }

                throw new Exception("Access token not valid");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add-accounts")]
        [CustomAuthorize(Common.Enums.Permission.UpdateGroup)]
        public async Task<IActionResult> AddAccounts([FromBody] AddAccountToGroupRequest request)
        {
            try
            {
                await _groupService.HandleAddAccounts(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}