using System.ComponentModel.DataAnnotations;

namespace Server.Common.Models
{
    public class GroupPermission
    {
        [Key]
        public long Id { get; set; }
        public long GroupId { get; set; }
        public long PermissionId { get; set; }
    }
}