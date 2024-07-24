using System.ComponentModel.DataAnnotations;

namespace Server.Common.Models
{
    public class Urban
    {
        [Key]
        public long Id { get; set; }
        public string? UrbanAddress { get; set; }

        public List<Building> Buildings { get; } = new();
    }
}