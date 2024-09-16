using Server.Common.Models;
using Server.DataAccess.Interfaces;

namespace Server.DataAccess.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly AppDbContext _db;

        public GroupRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddAccountGroups(List<AccountGroup> accountGroups)
        {
            _db.AddRange(accountGroups);
            await _db.SaveChangesAsync();
        }

        public async Task AddGroupPermissions(List<GroupPermission> groupPermissions)
        {
            _db.GroupPermissions.AddRange(groupPermissions);
            await _db.SaveChangesAsync();
        }

        public async Task<Group> CreateGroup(string? name)
        {
            Group group = new()
            {
                GroupName = name ?? ""
            };

            _db.Add(group);
            await _db.SaveChangesAsync();

            return group;
        }
    }
}