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
        private readonly Permission[] _requiredPermissions;

        public DatabasePermissionAuthorizationFilter(IAuthorizeRepository authorizeRepository, Permission[] requiredPermissions)
        {
            _authorizeRepository = authorizeRepository;
            _requiredPermissions = requiredPermissions;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var authorizationHeader = context.HttpContext.Request.Headers[HeaderNames.Authorization].ToString();

            if (string.IsNullOrWhiteSpace(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var accessToken = authorizationHeader["Bearer ".Length..].Trim();

            bool hasPermission = false;
            foreach (var requiredPermission in _requiredPermissions)
            {
                if (await _authorizeRepository.CheckAccountPermission(accessToken, requiredPermission.ToString()))
                {
                    hasPermission = true;
                    context.HttpContext.Items["UserPermission"] = requiredPermission;
                    break;
                }
            }

            if (!hasPermission)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}