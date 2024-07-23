using System.ComponentModel.DataAnnotations;

namespace Server.Common
{
    public class Document
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public required long ModuleId { get; set; }
        public DateTime Timestamp { get; set; }

        public Module Module { get; set; } = null!;
        public List<DocumentDetail> DocumentDetails { get; } = new();
    }
}