

using Server.Models.DTOs.Document;

namespace Server.Models.DTOs.Resident
{
    public class DetailsNewResidentResponse
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public required string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? AccountStatus { get; set; }
        public int RoomNumber { get; set; }
        public string? BuildingName { get; set; }
        public string? BuildingAddress { get; set; }
        public DateTime Timestamp { get; set; }
        public List<DocumentDetails> DocumentDetailsList { get; set; } = new();
    }
}