using Microsoft.AspNetCore.Mvc;
using Server.Presentation.Filters;

namespace Server.Presentation.CustomAuthorizes
{
    public class CustomAuthorizeAttribute : TypeFilterAttribute
    {
        public CustomAuthorizeAttribute(string permission) : base(typeof(DatabasePermissionAuthorizationFilter))
        {
            Arguments = new object[] { permission };
        }
    }
}