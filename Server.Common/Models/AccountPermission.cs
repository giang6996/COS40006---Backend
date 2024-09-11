using System.ComponentModel.DataAnnotations;

namespace Server.Common.Models
{
    public class AccountPermission
    {
        [Key]
        public long Id { get; set; }
        public long AccountId { get; set; }
        public long PermissionId { get; set; }
    }
}