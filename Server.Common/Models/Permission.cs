using System.ComponentModel.DataAnnotations;

namespace Server.Common.Models
{
    public class Permission
    {
        [Key]
        public long Id { get; set; }
        public required string PermissionName { get; set; }

        public List<Role> Roles { get; } = new();
        public List<Module> Modules { get; } = new();
        public List<Group> Groups { get; } = new();
        public List<Account> Accounts { get; } = new();
    }
}