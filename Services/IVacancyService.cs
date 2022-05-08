using FindJobWebApi.DTOs;

namespace FindJobWebApi.Services
{
    public interface IVacancyService
    {
        public string AddNewVacancy(int companyId, CreateVacancyDTO vacancyDTO);

        public string ModifyVacancy(int id, ModifyVacancyDTO vacancyDTO);

        public string DeleteVacancy(int companyId, int vacancyId);
    }
}
