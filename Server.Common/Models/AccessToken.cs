using System.ComponentModel.DataAnnotations;

namespace Server.Common.Models
{
    public class AccessToken
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public required long RtId { get; set; }
        [Required]
        public required string Value { get; set; }
        [Required]
        public required DateTime ExpirationDate { get; set; }
        [Required]
        public required bool Revoked { get; set; } = false;

        public RefreshToken? RefreshToken { get; set; } = null!;
    }
}