using Microsoft.AspNetCore.Mvc;
using Server.Common.Enums;
using Server.Presentation.Filters;

namespace Server.Presentation.CustomAttributes
{
    public class CustomAuthorizeAttribute : TypeFilterAttribute
    {
        public CustomAuthorizeAttribute(params Permission[] permissions) : base(typeof(DatabasePermissionAuthorizationFilter))
        {
            Arguments = new object[] { permissions };
        }
    }
}