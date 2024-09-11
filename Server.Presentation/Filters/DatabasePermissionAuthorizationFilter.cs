using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;
using Server.BusinessLogic.Interfaces;
using Server.DataAccess.Interfaces;

namespace Server.Presentation.Filters
{
    public class DatabasePermissionAuthorizationFilter : IAuthorizationFilter
    {
        private readonly IAuthorizeRepository _authorizeRepository;
        private readonly string _requiredPermission;

        public DatabasePermissionAuthorizationFilter(IAuthorizeRepository authorizeRepository, string requiredPermission)
        {
            _authorizeRepository = authorizeRepository;
            _requiredPermission = requiredPermission;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authorizationHeader = context.HttpContext.Request.Headers[HeaderNames.Authorization];

            if (authorizationHeader.ToString().StartsWith("Bearer"))
            {
                var accessToken = authorizationHeader.ToString()["Bearer ".Length..].Trim();

                if (!_authorizeRepository.CheckAccountPermission(accessToken, _requiredPermission))
                {
                    context.Result = new ForbidResult();
                }
            }
        }
    }
}