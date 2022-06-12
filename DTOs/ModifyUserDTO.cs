namespace FindJobWebApi.DTOs
{
    public class ModifyUserDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Image { get; set; }
        public DateTime? BirthdayDate { get; set; }
        public string? ContactNumber { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Gender { get; set; }
        public float? Experience { get; set; }
        public string? Password { get; set; }
        public string? Desciption { get; set; }
    }
}
