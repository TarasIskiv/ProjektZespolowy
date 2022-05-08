using AutoMapper;
using FindJobWebApi.DataBase;
using FindJobWebApi.DTOs;
using FindJobWebApi.Models;

namespace FindJobWebApi.Services
{
    public class VacancyService : IVacancyService
    {
        private readonly AppDBContext _context;
        private readonly IMapper _mapper;

        public VacancyService(AppDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public string AddNewVacancy(int companyId, CreateVacancyDTO vacancyDTO)
        {
            var currentVacancy = _mapper.Map<Vacancy>(vacancyDTO);
            currentVacancy.CompanyId = companyId;

            List<Vacancy> vacancies = new List<Vacancy>();

                
 

            int id = 0;

            if (_context.Vacancies.Count() != 0)
            {
                vacancies = _context.Vacancies.ToList();
                id = vacancies.OrderBy(x => x.Id).Last().Id;
            }      

            currentVacancy.Id = (int)(id + 1);

            currentVacancy.UpdateTime = DateTime.SpecifyKind(DateTime.Now.Date.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second), DateTimeKind.Utc);

            _context.Vacancies.Add(currentVacancy);
            _context.SaveChanges();

            return "OK";
        }

        public string ModifyVacancy(int companyId, ModifyVacancyDTO vacancyDTO)
        {
            var vacancy = _context.Vacancies.SingleOrDefault(x => x.Id == vacancyDTO.Id);
            if (vacancy == null) 
                return "Error";

            if (vacancy.CompanyId != companyId)
                return "Not yours";

            if (!string.IsNullOrEmpty(vacancyDTO.Title)) vacancy.Title = vacancyDTO.Title;
            if (!string.IsNullOrEmpty(vacancyDTO.Description)) vacancy.Description = vacancyDTO.Description;
            if (!string.IsNullOrEmpty(vacancyDTO.Requirements)) vacancy.Requirements = vacancyDTO.Requirements;
            if (!string.IsNullOrEmpty(vacancyDTO.Responsibilities)) vacancy.Responsibilities = vacancyDTO.Responsibilities;
            if (vacancyDTO.Salary != null) vacancy.Salary = (decimal)vacancyDTO.Salary;

            vacancy.UpdateTime = DateTime.SpecifyKind(DateTime.Now.Date.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second), DateTimeKind.Utc);

            _context.SaveChanges();

            return "OK";
        }

        public string DeleteVacancy(int companyId, int vacancyId) 
        {
            var vacancy = _context.Vacancies.SingleOrDefault(x => x.Id == vacancyId);
            if (vacancy == null)
                return "Error";

            if (vacancy.CompanyId != companyId)
                return "Not yours";

            _context.Vacancies.Remove(vacancy);
            _context.SaveChanges();

            return "OK";
        }
    }
}
