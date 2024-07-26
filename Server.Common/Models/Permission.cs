using System.ComponentModel.DataAnnotations;

namespace Server.Common.Models
{
    public class Permission
    {
        [Key]
        public long Id { get; set; }
        public required string PermissionName { get; set; }

        public List<Role> Roles { get; } = new();
    }
}