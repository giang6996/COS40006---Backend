using System.ComponentModel.DataAnnotations;

namespace Server.Common.Models
{
    public class ModulePermission
    {
        [Key]
        public long Id { get; set; }
        public long ModuleId { get; set; }
        public long PermissionId { get; set; }
    }
}