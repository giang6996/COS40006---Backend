using Server.Common.Models;

namespace Server.DataAccess.Interfaces
{
    public interface IPermissionRepository
    {
        Task<List<Permission>> FetchPermissions(List<Common.Enums.Permission> permissions);
    }
}