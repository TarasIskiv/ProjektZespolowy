namespace FindJobWebApi.DTOs
{
    public class VacancyDTO
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Requirements { get; set; }
        public string Responsibilities { get; set; }
        public decimal Salary { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
