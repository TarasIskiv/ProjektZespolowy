using System.ComponentModel.DataAnnotations;

namespace FindJobWebApi.Models
{
    public class User
    {
        
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string? Image { get; set; }
        public DateTime? BirthdayDate { get; set; } =default(DateTime?);
        public string? ContactNumber { get; set; }= string.Empty;
        public string? Country { get; set; } = string.Empty;
        public string? City { get; set; } = string.Empty;
        public string? Gender { get; set; } = string.Empty;
        public float? Experience { get; set; } = default(float);

        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string? Desciption { get; set; } = string.Empty;
    }
}
