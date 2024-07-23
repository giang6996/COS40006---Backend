using System.ComponentModel.DataAnnotations;

namespace Server.Common
{
    public class Resident
    {
        [Key]
        public long Id { get; set; }
        public long AccountId { get; set; }
        public long ApartmentId { get; set; }
        public long ModuleId { get; set; }

        public Account Account { get; set; } = null!;
        public Apartment Apartment { get; set; } = null!;
        public Module Module { get; set; } = null!;
    }
}