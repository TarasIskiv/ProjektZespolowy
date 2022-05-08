using FindJobWebApi.DTOs;
using FindJobWebApi.Models;

namespace FindJobWebApi.Services
{
    public interface ICompanyService
    {
        public string SignIn(LoginCompanyDTO companyDTO);

        public string SignUp(CreateCompanyDTO companyDTO);

        public void SubscribeCompany();

        public IEnumerable<CompanyDTO> GetCompanies();

        public IEnumerable<VacancyDTO> GetVacanciesByCompany(int id);

        public CompanyDTO GetProfile(int id);

        public string AddProfile(int id, ModifyCompanyDTO dto);

        public void UploadCompanyPhoto();

        public CompanyDTO GetCompanyById(int id);
    }
}
