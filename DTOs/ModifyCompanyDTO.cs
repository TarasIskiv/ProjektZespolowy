namespace FindJobWebApi.DTOs
{
    public class ModifyCompanyDTO
    {
        public string? CompanyName { get; set; }
        public int? CompanyAddressId { get; set; }
        public string? Website { get; set; }
        public string? Password { get; set; }
        public string? Description { get; set; }
    }
}
