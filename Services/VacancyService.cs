﻿using AutoMapper;
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

        public VacancyDTO? GetVacancyById(int id)
        {
            var vacancy = _context.Vacancies.SingleOrDefault(x => x.Id == id);

            if (vacancy == null) return null;

            var mappedVacancy = _mapper.Map<VacancyDTO>(vacancy);

            return mappedVacancy;
        }

        public IEnumerable<VacancyDTO>? GetVacanciesByFilters(int? minSalary, string? country, string? city, string? search)
        {
            var vacancies = _context.Vacancies.ToList();

            if (minSalary != null && minSalary != default(int) && minSalary > 0)
                vacancies = vacancies.Where(x => x.Salary >= minSalary).ToList();

            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                vacancies = vacancies.Where(x => x.Title.ToLower().Contains(search) ||
                                            x.Description.ToLower().Contains(search) ||
                                            x.Requirements.ToLower().Contains(search)).ToList();
            }

            vacancies.ForEach(vacancy => vacancy.Company = _context.Companies.Single(x => x.Id == vacancy.CompanyId));

            if (!string.IsNullOrEmpty(country))
            {
                country = country.ToLower();
                vacancies = vacancies.Where(x => !string.IsNullOrEmpty(x.Company.Country) && x.Company.Country.ToLower().Contains(country)).ToList();
            }

            if (!string.IsNullOrEmpty(city))
            {
                city = city.ToLower();
                vacancies = vacancies.Where(x => !string.IsNullOrEmpty(x.Company.City) && x.Company.City.ToLower().Contains(city)).ToList();
            }

            if (vacancies.Count == 0)
                return null;

            return _mapper.Map<List<VacancyDTO>>(vacancies);
        }

        public string SubscribeVacancy(SubcribeCandidateDTO candidateDTO)
        {
            if (!_context.Vacancies.Any(x => x.Id == candidateDTO.VacancyId))
                return "Vacancy is not exist";

            if (!_context.Users.Any(x => x.Id == candidateDTO.UserId))
                return "User is not exist";

            if(_context.Candidtates.Any(x => x.UserId == candidateDTO.UserId && x.VacancyId == candidateDTO.VacancyId))
                return "User already subscribed";

            var candidate = _mapper.Map<Candidtate>(candidateDTO);

            _context.Candidtates.Add(candidate);
            _context.SaveChanges();

            return "OK";
        }

        public string UnsubscribeVacancy()
        {
            throw new NotImplementedException();
        }

        public string GetCandidates(int id, out List<UserDTO> result)
        {
            result = new List<UserDTO>();

            if (!_context.Vacancies.Any(x => x.Id == id))
                return "Vacancy is not exist";

            var candidates = _context.Candidtates.Where(x => x.VacancyId == id).ToList();

            if (candidates.Count == 0)
                return "Nobody subscribed for this vacancy";

            foreach (var candidate in candidates)
            {
                var user = _mapper.Map<UserDTO>(_context.Users.Single(x => x.Id == candidate.UserId));
                result.Add(user);
            }

            return "OK";
        }
    }
}
