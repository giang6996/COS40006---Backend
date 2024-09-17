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
        public List<FormResidentRequest> FormResidentRequests { get; } = new();
        public Resident? Resident { get; set; }
        public List<Role> Roles { get; } = new();
        public List<RefreshToken> RefreshTokens { get; } = new();
        public List<Group> Groups { get; } = new();
        public List<Permission> Permissions { get; } = new();
        public Tenant? Tenant { get; set; }
    }
}