using FindJobWebApi.DTOs;

namespace FindJobWebApi.Services
{
    public interface IUserService
    {
        public string SignIn(LoginUserDTO userDTO);

        public string SignUp(CreateUserDTO userDTO);

        public UserDTO GetUser(int id);

        public IEnumerable<UserDTO> GetUsers();

        public IEnumerable<UserDTO>? GetUsersByFilters(string country, string city, string gender, float experience, string search);

        public string AddProfile(int id, ModifyUserDTO dto);
    }
}
