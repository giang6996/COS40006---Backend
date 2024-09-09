using System.ComponentModel.DataAnnotations;

namespace Server.Common.Models
{
    public class FormResidentRequestDetail
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public required long FormResidentRequestId { get; set; }
        public required string Title { get; set; }
        public required string TYpe { get; set; }
        public required string Description { get; set; }
        public required string Status { get; set; }
        public string? RequestMediaLink { get; set; }
        public string? Response { get; set; }

        public FormResidentRequest FormResidentRequest { get; set; } = null!;
    }
}