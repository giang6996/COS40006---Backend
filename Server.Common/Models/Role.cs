// For basic implementation
using System.ComponentModel.DataAnnotations;

namespace Server.Common.Models
{
    public class Role
    {
        [Key]
        public long Id { get; set; }
        public long AccountId { get; set; }
        public required string Name { get; set; }


        public Account Account { get; set; } = null!;
    }
}