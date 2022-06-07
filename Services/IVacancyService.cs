using FindJobWebApi.DTOs;

namespace FindJobWebApi.Services
{
    public interface IVacancyService
    {
        public string AddNewVacancy(int companyId, CreateVacancyDTO vacancyDTO);

        public string ModifyVacancy(int id, ModifyVacancyDTO vacancyDTO);

        public string DeleteVacancy(int companyId, int vacancyId);

        public VacancyDTO? GetVacancyById(int id);

        public IEnumerable<VacancyDTO>? GetVacanciesByFilters(int? minSalary, string? country, string? city, string? search);

        public string SubscribeVacancy(SubcribeCandidateDTO candidateDTO);

        public string UnsubscribeVacancy();

        public string GetCandidates(int id, out List<UserDTO> result);
    }
}
