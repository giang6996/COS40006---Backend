using System.ComponentModel.DataAnnotations;

namespace Server.Common.Models
{
    public class Apartment
    {
        [Key]
        public long Id { get; set; }
        public long BuildingId { get; set; }
        public string? Available { get; set; }
        public int RoomNumber { get; set; }

        public Building Building { get; set; } = null!;
        public ApartmentDetail? ApartmentDetail { get; set; }
        public List<Resident> Residents { get; } = new();
    }
}