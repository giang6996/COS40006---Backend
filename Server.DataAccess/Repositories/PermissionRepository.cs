using Microsoft.EntityFrameworkCore;
using Server.Common.Models;
using Server.DataAccess.Interfaces;

namespace Server.DataAccess.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly AppDbContext _db;

        public PermissionRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Permission>> FetchPermissions(List<Common.Enums.Permission> permissions)
        {
            var permissionStrings = permissions.Select(p => p.ToString()).ToList();

            var permissionsFromDb = await _db.Permissions
                .Where(p => permissionStrings.Contains(p.PermissionName))
                .ToListAsync();

            return permissionsFromDb;
        }
    }
}