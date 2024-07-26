// For basic implementation
using System.ComponentModel.DataAnnotations;

namespace Server.Common.Models
{
    public class Role
    {
        [Key]
        public long Id { get; set; }
        public required string Name { get; set; }

        public List<Account> Accounts { get; } = new();
        public List<Permission> Permissions { get; } = new();
    }
}