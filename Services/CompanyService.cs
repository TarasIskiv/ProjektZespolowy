using AutoMapper;
using FindJobWebApi.DataBase;
using FindJobWebApi.DTOs;
using FindJobWebApi.Models;
using FindJobWebApi.Secure;
namespace FindJobWebApi.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly AppDBContext _context;
        private readonly IMapper _mapper;

        public CompanyService(AppDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        public string AddProfile(int id,ModifyCompanyDTO dto)
        {
            var company = _context.Companies.SingleOrDefault(x => x.Id == id);
            if (company == null) return "Error";

            if(!string.IsNullOrEmpty(dto.Description)) company.Description = dto.Description;
            if(!string.IsNullOrEmpty(dto.CompanyName)) company.CompanyName = dto.CompanyName;
            if (!string.IsNullOrEmpty(dto.Image)) company.Image = dto.Image;

            if (!string.IsNullOrEmpty(dto.Country)) company.Country = dto.Country;
            if (!string.IsNullOrEmpty(dto.City)) company.City = dto.City;

            if (!string.IsNullOrEmpty(dto.Website)) company.Website = dto.Website;
            if (!string.IsNullOrEmpty(dto.Password)) company.Password = dto.Password.getHash();

            _context.SaveChanges();

            return "OK";
        }

        public IEnumerable<CompanyDTO> GetCompanies()
        {
            var companies = _context.Companies.ToList();
            var mappedCompanies = _mapper.Map<List<CompanyDTO>>(companies);

            return mappedCompanies;
        }

        public IEnumerable<CompanyDTO>? GetCompaniesByFilters(string? country, string? city, string? search)
        {
            var companies = _context.Companies.ToList();

            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                companies = companies.Where(x => x.CompanyName.ToLower().Contains(search) ||
                                            (!string.IsNullOrEmpty(x.Description) && x.Description.ToLower().Contains(search))).ToList();
            }

            if (!string.IsNullOrEmpty(country))
            {
                country = country.ToLower();
                companies = companies.Where(x => !string.IsNullOrEmpty(x.Country) && x.Country.ToLower().Contains(country)).ToList();
            }

            if (!string.IsNullOrEmpty(city))
            {
                city = city.ToLower();
                companies = companies.Where(x => !string.IsNullOrEmpty(x.City) && x.City.ToLower().Contains(city)).ToList();
            }

            if (companies.Count == 0)
                return null;

            return _mapper.Map<List<CompanyDTO>>(companies);
        }

        public CompanyDTO GetCompanyById(int id)
        {
            var company = _context.Companies.SingleOrDefault(x => x.Id == id);
            if (company == null) return null;

            var mappedCompany = _mapper.Map<CompanyDTO>(company);

            return mappedCompany;
        }

        public CompanyDTO GetProfile(int id)
        {
            var company = _context.Companies.SingleOrDefault(x => x.Id == id);
            if(company == null) return null;

            var mappedCompany = _mapper.Map<CompanyDTO>(company);
            
            return mappedCompany;
        }

        public IEnumerable<VacancyDTO> GetVacanciesByCompany(int id)
        {
            var vacancies = _context.Vacancies.Where<Vacancy>(x => x.CompanyId == id);
            var mappedVacancies = _mapper.Map<List<VacancyDTO>>(vacancies);

            return mappedVacancies;
        }

        public string SignIn(LoginCompanyDTO companyDTO)
        {
           var currentCompany = _context.Companies.SingleOrDefault(x => x.Email.Equals(companyDTO.Email));
           var hashedPassword = companyDTO.Password.getHash();
            if (currentCompany == null || !hashedPassword.Equals(currentCompany.Password))
            {
                return "Login/Password is incorrect!";
            }
            return currentCompany.Id.ToString();
        }

        public string SignUp(CreateCompanyDTO companyDTO)
        {
            if (_context.Companies.Any(x => x.Email == companyDTO.Email))
            {
                return "Account with this email address already exists!";
            }

            var currentFirm = _mapper.Map<Company>(companyDTO);
            currentFirm.Password = companyDTO.Password.getHash();
            _context.Companies.Add(currentFirm);
            _context.SaveChanges();
            return "OK";
        }

        public void SubscribeCompany()
        {
            throw new NotImplementedException();
        }

        public void UploadCompanyPhoto()
        {
            throw new NotImplementedException();
        }
    }
}
