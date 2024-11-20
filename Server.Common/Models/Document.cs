using System.ComponentModel.DataAnnotations;

namespace Server.Common.Models
{
    public class Document
    {
        //[Key]
        //public long Id { get; set; }
        //[Required]
        //public required long AccountId { get; set; }
        //[Required]
        //public required long ModuleId { get; set; }
        //public DateTime Timestamp { get; set; }
        //public int RoomNumber { get; set; }
        //public string? BuildingName { get; set; }
        //public string? BuildingAddress { get; set; }

        //public Account Account { get; set; } = null!;
        //public Module Module { get; set; } = null!;
        //public List<DocumentDetail> DocumentDetails { get; } = new();

        [Key]
        public long Id { get; set; }

        [Required]
        public required long AccountId { get; set; }

        [Required]
        public required long ModuleId { get; set; }

        public DateTime Timestamp { get; set; }

        // Foreign key for Apartment
        public long ApartmentId { get; set; }

        // Foreign key for Building
        public long BuildingId { get; set; }

        // Navigation properties
        public Account Account { get; set; } = null!;
        public Module Module { get; set; } = null!;
        public Apartment Apartment { get; set; } = null!;
        public Building Building { get; set; } = null!;
        public List<DocumentDetail> DocumentDetails { get; } = new();

    }
}