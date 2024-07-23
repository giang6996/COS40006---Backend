using System.ComponentModel.DataAnnotations;

namespace Server.Common
{
    public class ComplaintDetail
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public required long ComplaintId { get; set; }
        public required string ComplaintDesc { get; set; }
        public required string Status { get; set; }
        public string? ComplaintMediaLink { get; set; }
        public string? Response { get; set; }

        public Complaint Complaint { get; set; } = null!;
    }
}