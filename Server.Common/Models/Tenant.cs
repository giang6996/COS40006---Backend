namespace Server.Common.Models
{
    public class Tenant
    {
        public long Id { get; set; }
        public required string Name { get; set; }

        public List<Account> Accounts { get; } = new();
        public List<Building> Buildings { get; } = new();
        public List<Group> Groups { get; } = new();
    }
}