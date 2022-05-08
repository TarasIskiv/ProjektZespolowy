using System.ComponentModel.DataAnnotations;

namespace FindJobWebApi.Models
{
    public class Vacancy
    {

        public int Id { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
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
        [Required]
        public DateTime UpdateTime { get; set; }

    }
}
