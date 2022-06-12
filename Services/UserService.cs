using AutoMapper;
using FindJobWebApi.DataBase;
using FindJobWebApi.DTOs;
using FindJobWebApi.Models;
using FindJobWebApi.Secure;

namespace FindJobWebApi.Services
{
    public class UserService : IUserService
    {
        private readonly AppDBContext _context;
        private readonly IMapper _mapper;

        public UserService(AppDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public string SignIn(LoginUserDTO userDTO)
        {
           var currentUser = _context.Users.SingleOrDefault(x => x.Email.Equals(userDTO.Email));
           var hashedPassword = userDTO.Password.getHash();
            if (currentUser == null || !hashedPassword.Equals(currentUser.Password))
            {
                return "Login/Password is incorrect!";
            }
            return currentUser.Id.ToString();
        }

        public string SignUp(CreateUserDTO userDTO)
        {
            if (_context.Users.Any(x => x.Email == userDTO.Email))
            {
                return "Account with this email address already exists!";
            }

            var currentUser = _mapper.Map<User>(userDTO);
            currentUser.Password = userDTO.Password.getHash();
            _context.Users.Add(currentUser);
            _context.SaveChanges();
            return "OK";
        }

        public UserDTO GetUser(int id)
        {
            var user = _context.Users.SingleOrDefault(x => x.Id == id);

            if (user == null) return null;

            var mappedUser = _mapper.Map<UserDTO>(user);

            return mappedUser;
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            var users = _context.Users.ToList();
            var mappedUsers = _mapper.Map<List<UserDTO>>(users);

            return mappedUsers;
        }

        public IEnumerable<UserDTO>? GetUsersByFilters(string? country, string? city, string? gender, float? experience, string? search)
        {
            var users = _context.Users.ToList();

            if (experience != null && experience != default(float) && experience > 0)
                users = users.Where(x => x.Experience >= experience).ToList();

            if (!string.IsNullOrEmpty(gender))
                users = users.Where(x => x.Gender == gender).ToList();

            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                users = users.Where(x => !string.IsNullOrEmpty(x.Desciption) && x.Desciption.ToLower().Contains(search)).ToList();
            }

            if (!string.IsNullOrEmpty(country))
            {
                country = country.ToLower();
                users = users.Where(x => !string.IsNullOrEmpty(x.Country) && x.Country.ToLower() == country).ToList();
            }

            if (!string.IsNullOrEmpty(city))
            {
                city = city.ToLower();
                users = users.Where(x => !string.IsNullOrEmpty(x.City) && x.City.ToLower() == city).ToList();
            }

            if (users.Count == 0)
                return null;

            return _mapper.Map<List<UserDTO>>(users);
        }

        public string AddProfile(int id, ModifyUserDTO dto)
        {
            var user = _context.Users.SingleOrDefault(x => x.Id == id);
            if (user == null) 
                return "Error";

            if (!string.IsNullOrEmpty(dto.FirstName)) user.FirstName = dto.FirstName;
            if (!string.IsNullOrEmpty(dto.LastName)) user.LastName = dto.LastName;
            if (!string.IsNullOrEmpty(dto.Image)) user.Image = dto.Image;
            if (!string.IsNullOrEmpty(dto.BirthdayDate.ToString())) user.BirthdayDate = dto.BirthdayDate;
            if (!string.IsNullOrEmpty(dto.ContactNumber)) user.ContactNumber = dto.ContactNumber;


            if (!string.IsNullOrEmpty(dto.Country)) user.Country = dto.Country;
                
            if (!string.IsNullOrEmpty(dto.City)) user.City = dto.City;

            if (!string.IsNullOrEmpty(dto.Gender)) user.Gender = dto.Gender;
            if (!string.IsNullOrEmpty(dto.Experience.ToString())) user.Experience = dto.Experience;
            if (!string.IsNullOrEmpty(dto.Password)) user.Password = dto.Password.getHash();
            if (!string.IsNullOrEmpty(dto.Desciption)) user.Desciption = dto.Desciption;

            _context.SaveChanges();

            return "OK";
        }
    }
}
