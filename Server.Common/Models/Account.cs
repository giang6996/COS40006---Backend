using System.ComponentModel.DataAnnotations;

namespace Server.Common
{
    public class Account
    {
        [Key]
        public long Id { get; set; }
        public long TenantId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Required]
        public required string Password { get; set; }
        [Required]
        public required string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Status { get; set; }

        public List<Module> Modules { get; } = new();
        public Resident? Resident { get; set; }
        public Role? Role { get; set; }
        public List<RefreshToken> RefreshTokens { get; } = new();
    }
}