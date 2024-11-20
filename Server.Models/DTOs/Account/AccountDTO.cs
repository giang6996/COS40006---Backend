using Server.Models.DTOs.Document;

namespace Server.Models.DTOs.Account
{
    public class AccountDTO
    {
        public long Id { get; set; }
        public long TenantId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public required string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Status { get; set; }
        public string? Apartment { get; set; }      // New field for Apartment
        public string? Building { get; set; }       // New field for Building
        public List<string>? Roles { get; set; } // New roles property

        public List<DocumentDetails> Documents { get; set; } = new List<DocumentDetails>();
    }
}