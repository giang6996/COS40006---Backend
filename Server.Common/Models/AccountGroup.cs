using System.ComponentModel.DataAnnotations;

namespace Server.Common.Models
{
    public class AccountGroup
    {
        [Key]
        public long Id { get; set; }
        public long AccountId { get; set; }
        public long GroupId { get; set; }
    }
}