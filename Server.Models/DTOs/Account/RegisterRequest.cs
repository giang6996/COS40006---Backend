namespace Server.Models.DTOs.Account
{
    public class RegisterRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Status { get; set; }
    }
}