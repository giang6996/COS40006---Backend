using System.ComponentModel.DataAnnotations;

namespace Server.Common
{
    public class Module
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public required string ModuleName { get; set; }
        public string? ModuleDesc { get; set; }

        public List<Account> Accounts { get; } = new();
        public List<Document> Documents { get; } = new();
        public List<Complaint> Complaints { get; } = new();
        public List<Building> Buildings { get; } = new();
        public List<Resident> Residents { get; } = new();
    }
}