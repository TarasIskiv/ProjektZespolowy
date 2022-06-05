namespace FindJobWebApi.DTOs
{
    public class CreateCVDTO
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
        public string Bio { get; set; }
        public int Template { get; set; }
    }
}
