using System.ComponentModel;
using System.Data.Common;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Server.Common.Models;
using Server.DataAccess.Interfaces;

namespace Server.DataAccess.Repositories
{
    public class AuthorizeRepository : IAuthorizeRepository
    {
        private readonly AppDbContext _db;

        public AuthorizeRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AssignAccountRole(Account account, Common.Enums.Role roleName)
        {
            Role role = await GetRoleByName(roleName);

            AccountRole accountRole = new()
            {
                AccountId = account.Id,
                RoleId = role.Id
            };

            _db.AccountRoles.Add(accountRole);
            await _db.SaveChangesAsync();
        }

        public async Task<Role> GetRoleByName(Common.Enums.Role roleName)
        {
            return await _db.Roles.Where(r => r.Name == roleName.ToString()).FirstOrDefaultAsync() ?? throw new Exception("Role not found");
        }

        public async Task<Role> GetRoleOfAccount(Account account, Common.Enums.Role roleName)
        {
            try
            {
                var role = await (
                    from a in _db.Accounts
                    join ar in _db.AccountRoles on a.Id equals ar.AccountId
                    join r in _db.Roles on ar.RoleId equals r.Id
                    where a.Id == account.Id && r.Name == roleName.ToString()
                    select r
                ).FirstOrDefaultAsync();

                return role ?? throw new Exception("Role of this account not found");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> VerifyModulePermission(Account account, Common.Enums.Role roleName, Common.Enums.Permission permissionName)
        {
            try
            {
                Role role = await GetRoleOfAccount(account, roleName);

                RolePermission? rolePermission = await (
                    from r in _db.Roles
                    join rp in _db.RolePermissions on r.Id equals rp.RoleId
                    join p in _db.Permissions on rp.PermissionId equals p.Id
                    where p.PermissionName == permissionName.ToString() && r.Id == role.Id
                    select rp
                ).FirstOrDefaultAsync();

                if (rolePermission != null)
                {
                    return true;
                }

                throw new Exception("No Permission");
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}