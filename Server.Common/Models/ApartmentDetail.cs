using System.ComponentModel.DataAnnotations;

namespace Server.Common
{
    public class ApartmentDetail
    {
        [Key]
        public long Id { get; set; }
        public long ApartmentId { get; set; }
        public string? Type { get; set; }
        public int Room { get; set; }
        public double Size { get; set; }

        public Apartment Apartment { get; set; } = null!;
    }
}