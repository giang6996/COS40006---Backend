using Server.Common.Models;

namespace Server.DataAccess.Interfaces
{
    public interface IGroupRepository
    {
        Task<Group> CreateGroup(string? name);

        Task AddGroupPermissions(List<GroupPermission> groupPermissions);
        Task AddAccountGroups(List<AccountGroup> accountGroups);
    }
}