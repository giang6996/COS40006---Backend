
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTOs.Account
{
    public class RegisterRequest
    {
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        // New fields for building and apartment selection
        [Required]
        public long BuildingId { get; set; }

        [Required]
        public long ApartmentId { get; set; }

        // Documents
        public List<IFormFile> Documents { get; set; } = new List<IFormFile>();
    }
}