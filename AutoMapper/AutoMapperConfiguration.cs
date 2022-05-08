using AutoMapper;
using FindJobWebApi.DTOs;
using FindJobWebApi.Models;

namespace FindJobWebApi.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static IMapper Initialize()
        {
            return new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<CreateCompanyDTO, Company>();
                    cfg.CreateMap<Company, CompanyDTO>();
                    cfg.CreateMap<CreateUserDTO, User>();
                    cfg.CreateMap<User, UserDTO>();
                    cfg.CreateMap<CompanyAddressDTO, CompanyAddress>();
                    cfg.CreateMap<CreateVacancyDTO, Vacancy>();
                    cfg.CreateMap<Vacancy, VacancyDTO>();
                }
            ).CreateMapper();
        }
    }
}
