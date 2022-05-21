using FindJobWebApi.Models;

namespace FindJobWebApi.DTOs
{
    public class CompanyDTO
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string? Description { get; set; }
    }
}
