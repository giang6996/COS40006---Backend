using System.ComponentModel.DataAnnotations;

namespace Server.Common
{
    public class AccountModule
    {
        [Key]
        public long Id { get; set; }
        public long AccountId { get; set; }
        public long ModuleId { get; set; }
    }
}