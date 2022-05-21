using System;
using System.ComponentModel.DataAnnotations;

namespace FindJobWebApi.Models
{
    public class Company 
    {
        public int Id { get; set; }

        [Required]
        public string CompanyName { get; set; } = String.Empty;
        public string? Country { get; set; } = string.Empty;
        public string? City { get; set; } = string.Empty;
        public string? Website { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = String.Empty;
        public string? Description { get; set; }
    }
}
