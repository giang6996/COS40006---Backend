using System.ComponentModel.DataAnnotations;

namespace Server.Common.Models
{
    public class RolePermission
    {
        [Key]
        public long Id { get; set; }
        public long RoleId { get; set; }
        public long PermissionId { get; set; }
    }
}