using System.ComponentModel.DataAnnotations;

namespace Server.Common.Models
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
        public List<Document> Documents { get; } = new();
        public List<Complaint> Complaints { get; } = new();
        public Resident? Resident { get; set; }
        public List<Role> Roles { get; } = new();
        public List<RefreshToken> RefreshTokens { get; } = new();
    }
}