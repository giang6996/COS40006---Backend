using Microsoft.AspNetCore.Mvc;
using Server.Common.Enums;
using Server.Presentation.Filters;

namespace Server.Presentation.CustomAttributes
{
    public class CustomAuthorizeAttribute : TypeFilterAttribute
    {
        public CustomAuthorizeAttribute(Permission permission) : base(typeof(DatabasePermissionAuthorizationFilter))
        {
            Arguments = new object[] { permission };
        }
    }
}