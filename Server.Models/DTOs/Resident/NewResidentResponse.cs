namespace Server.Models.DTOs.Resident
{
    public class NewResidentResponse
    {
        public string? FullName { get; set; }
        public required string Email { get; set; }
    }
}