using System.Diagnostics.Contracts;

namespace Server.Models.DTOs.Resident
{
    public class UpdateAccountStatusRequest
    {
        public required long AccountId { get; set; }
        public required string Status { get; set; }
    }
}