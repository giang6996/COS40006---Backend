using System.ComponentModel.DataAnnotations;

namespace Server.Common.Models
{
    public class Building
    {
        [Key]
        public long Id { get; set; }
        public long TenantId { get; set; }
        public long UrbanId { get; set; }
        public long ModuleId { get; set; }
        public int NumberFloor { get; set; }
        public string? BuildingName { get; set; }
        public string? BuildingAddress { get; set; }

        public Module Module { get; set; } = null!;
        public Urban Urban { get; set; } = null!;
        public List<Apartment> Apartments { get; } = new();
        public Tenant Tenant { get; set; } = null!;
    }
}