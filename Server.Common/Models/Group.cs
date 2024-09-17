using System.ComponentModel.DataAnnotations;

namespace Server.Common.Models
{
    public class Group
    {
        [Key]
        public long Id { get; set; }
        public string? GroupName { get; set; }
        public long TenantId { get; set; }

        public List<Account> Accounts { get; } = new();
        public List<Permission> Permissions { get; } = new();
        public Tenant Tenant { get; set; } = null!;
    }
}