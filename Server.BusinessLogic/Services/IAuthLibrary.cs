using System.Security.Claims;
using Server.Common;

namespace Server.BusinessLogic.Services
{
    public interface IAuthLibrary
    {
        public object Generate(Account account, bool includeRefreshToken = true);
        public ClaimsPrincipal? Validate(string accessToken, bool validateLifetimeParam = false);
    }
}