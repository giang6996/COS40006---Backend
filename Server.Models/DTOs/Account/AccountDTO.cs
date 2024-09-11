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
    }
}