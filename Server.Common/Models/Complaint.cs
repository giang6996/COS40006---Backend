using System.ComponentModel.DataAnnotations;

namespace Server.Common.Models
{
    public class Complaint
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public required long AccountId { get; set; }
        [Required]
        public required long ModuleId { get; set; }
        public DateTime Timestamp { get; set; }

        public Account Account { get; set; } = null!;
        public Module Module { get; set; } = null!;
        public List<ComplaintDetail> ComplaintDetails { get; } = new();
    }
}