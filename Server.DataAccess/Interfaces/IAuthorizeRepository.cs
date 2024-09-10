using Server.Common.Enums;
using Server.Common.Models;
using Role = Server.Common.Models.Role;

namespace Server.DataAccess.Interfaces
{
    public interface IAuthorizeRepository
    {
        Task<Role> GetRoleByName(Common.Enums.Role roleName);
        Task<Role> GetRoleOfAccount(Account account, Common.Enums.Role roleName);
        Task<Role> FetchRoleFromAccount(Account account);
        Task<bool> VerifyModulePermission(Account account, Common.Enums.Role roleName, Common.Enums.Permission permissionName);
        Task AssignAccountRole(Account account, Common.Enums.Role roleName);
    }
}