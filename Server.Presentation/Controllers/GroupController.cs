using System.Security.Claims;
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
        private readonly IAuthLibraryService _authLibraryService;
        private readonly IGroupService _groupService;

        public GroupController(IAuthLibraryService authLibraryService, IGroupService groupService)
        {
            _authLibraryService = authLibraryService;
            _groupService = groupService;
        }

        [CustomAuthorize(Common.Enums.Permission.CreateGroup)]
        [HttpPost]
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

                return BadRequest("Access token not valid");
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpGet]
        [CustomAuthorize(Common.Enums.Permission.UpdateGroup)]
        public IActionResult AddAccounts([FromBody] List<long> accountIds)
        {
            throw new NotImplementedException();
        }
    }
}