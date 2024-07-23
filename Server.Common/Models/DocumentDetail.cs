using System.ComponentModel.DataAnnotations;

namespace Server.Common
{
    public class DocumentDetail
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public required long DocumentId { get; set; }
        public required string Name { get; set; }
        public string? DocumentDesc { get; set; }
        public required string Status { get; set; }
        public string? DocumentLink { get; set; }

        public Document Document { get; set; } = null!;
    }
}