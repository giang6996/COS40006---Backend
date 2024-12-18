using System.ComponentModel.DataAnnotations;

namespace Server.Common.Models
{
    public class AccountRole
    {
        [Key]
        public long Id { get; set; }
        public long AccountId { get; set; }
        public long RoleId { get; set; }

        // Navigation properties
        public Account Account { get; set; } = default!;
        public Role Role { get; set; } = default!;
    }
}