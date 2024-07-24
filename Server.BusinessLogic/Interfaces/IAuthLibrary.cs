using System.Security.Claims;
using Server.Common.Models;

namespace Server.BusinessLogic.Interfaces
{
    public interface IAuthLibrary
    {
        public object Generate(Account account, bool includeRefreshToken = true);
        public ClaimsPrincipal? Validate(string accessToken, bool validateLifetimeParam = false);
    }
}