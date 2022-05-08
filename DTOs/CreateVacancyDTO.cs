using System.ComponentModel.DataAnnotations;

namespace FindJobWebApi.DTOs
{
    public class CreateVacancyDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Requirements { get; set; }
        [Required]
        public string Responsibilities { get; set; }
        [Required]
        public decimal Salary { get; set; }
    }
}
