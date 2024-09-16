using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;
using Server.Common.Enums;
using Server.DataAccess.Interfaces;

namespace Server.Presentation.Filters
{
    public class DatabasePermissionAuthorizationFilter : IAsyncAuthorizationFilter
    {
        private readonly IAuthorizeRepository _authorizeRepository;
        private readonly Permission _requiredPermission;

        public DatabasePermissionAuthorizationFilter(IAuthorizeRepository authorizeRepository, Permission requiredPermission)
        {
            _authorizeRepository = authorizeRepository;
            _requiredPermission = requiredPermission;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var authorizationHeader = context.HttpContext.Request.Headers[HeaderNames.Authorization];

            if (authorizationHeader.ToString().StartsWith("Bearer"))
            {
                var accessToken = authorizationHeader.ToString()["Bearer ".Length..].Trim();

                if (!await _authorizeRepository.CheckAccountPermission(accessToken, _requiredPermission.ToString()))
                {
                    context.Result = new ForbidResult();
                }
            }
        }
    }
}