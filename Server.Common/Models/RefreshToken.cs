using System.ComponentModel.DataAnnotations;

namespace Server.Common.Models
{
    public class RefreshToken
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public required string Value { get; set; }
        [Required]
        public required long AccountId { get; set; }
        [Required]
        public required DateTime ExpirationDate { get; set; }
        [Required]
        public required bool Revoked { get; set; } = false;


        public Account? Account { get; set; } = null!;
        public List<AccessToken>? AccessTokens { get; } = new();
    }
}

