using FindJobWebApi.DTOs;
using FindJobWebApi.Models;
using FindJobWebApi.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Newtonsoft.Json.Serialization;
using FindJobWebApi.Response;
using FindJobWebApi.JWTLogic;

namespace FindJobWebApi.Controllers
{
    [Route("api/company")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _service;
        private readonly ITokenService _tokenService;

        private readonly ICookieService _cookieService;

        public CompanyController(ICompanyService service, ITokenService tokenService, ICookieService cookieService)
        {
            _service = service;
           _tokenService = tokenService;
            _cookieService = cookieService;
        }
        #region Sign In
        [AllowAnonymous]
        [HttpPost("signin")]
        public async Task<ActionResult<string>> Signin([FromBody] LoginCompanyDTO companyDTO)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();
                return BadRequest(ResponseConvertor.GetResult("error", errors));
            }
            var result = _service.SignIn(companyDTO);


            if (!int.TryParse(result, out int id))
            {
                return BadRequest(ResponseConvertor.GetResult("error", result));
            }

            var token = _tokenService.generateToken(id, "Company");

            var claimsPrincipal = _cookieService.GetClaimsPrincipal("Company", token);
            await HttpContext.SignInAsync("Cookie", claimsPrincipal);

            return Ok(ResponseConvertor.GetResult("OK", token));    
        }
        #endregion
        #region SignUp
        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<ActionResult<string>> SignUp([FromBody] CreateCompanyDTO companyDTO)
        {
            if(!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();

                return BadRequest(ResponseConvertor.GetResult("error", errors));
            }

            var result = _service.SignUp(companyDTO);

            if (!result.Equals("OK")) return Conflict(ResponseConvertor.GetResult("error", result));
            
            return Ok(ResponseConvertor.GetResult("OK", result));
        }
        #endregion
        #region Subscribe

        [HttpPost("{id}/subscribe")]
        public async Task<ActionResult<string>> SubscribeToNewVacancies([FromRoute] int id)
        {
            
            return "SubscribeToNewVacancies";
        }
        #endregion
        #region Get Company By ID
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyDTO>> GetCompanyById([FromRoute] int id)
        {
            if (id < 1) return BadRequest(ResponseConvertor.GetResult("error", "impossible Id value"));

            var company = _service.GetCompanyById(id);
            if (company == null) return BadRequest(ResponseConvertor.GetResult("error", "Problem occured by company ID"));
            
            return Ok(ResponseConvertor.GetResult("OK", company));
        }
        #endregion
        #region Get Companies
        [AllowAnonymous]
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<CompanyDTO>>> GetAllCompanies()
        {
            var companies = _service.GetCompanies();
            if (companies == null) return BadRequest(ResponseConvertor.GetResult("error", "There aren't found companies"));

            return Ok(ResponseConvertor.GetResult("OK", companies));
        }
        #endregion

        #region Get List of Vacancies
        [AllowAnonymous]
        [HttpGet("{id}/job")]
        public async Task<ActionResult<IEnumerable<VacancyDTO>>> ListOfVacanciesOfCompany([FromRoute] int id)
        {
            var company = _service.GetCompanyById(id);

            if (company == null) 
                return BadRequest(ResponseConvertor.GetResult("error", "Problem occured by company ID"));

            var vacancies = _service.GetVacanciesByCompany(id);

            return Ok(ResponseConvertor.GetResult("OK", vacancies));
        }
        #endregion

        #region Get Profile
        [Authorize(Roles = "Company")]
        [HttpGet("profile")]
        public async Task<ActionResult<CompanyDTO>> GetCompanyProfile()
        {

            var currentCompany = User.Identity;
            var currentId = Int32.MinValue;

            if (currentCompany == null || !Int32.TryParse(currentCompany.Name, out currentId))
                return NotFound(ResponseConvertor.GetResult("error", "Problem occured by token"));

            var company = _service.GetProfile(currentId);

            if(company == null)
                return NotFound(ResponseConvertor.GetResult("error", "Problem occured by company ID"));

            return Ok(ResponseConvertor.GetResult("OK", company));
        }
        #endregion

        #region Add Data About Profile
        [Authorize(Roles = "Company")]
        [HttpPost("profile")]
        public async Task<ActionResult<string>> AddDataAboutCompany(ModifyCompanyDTO dto)
        {
            var currentCompany = User.Identity;
            var currentId = Int32.MinValue;

            if (currentCompany == null || !Int32.TryParse(currentCompany.Name, out currentId))
                return NotFound(ResponseConvertor.GetResult("error", "Problem occured by token"));

            var result = _service.AddProfile(currentId, dto);

            if(result.Equals("Error")) 
                return NotFound(ResponseConvertor.GetResult("error", "Problem occured by company ID"));

            return Ok(ResponseConvertor.GetResult("OK", result));

        }
        #endregion

        #region Upload Company Photo
        [HttpPut("profile/upload")]
        public async Task<ActionResult<string>> UploadCompanyPhoto()
        {
            return "UploadCompanyPhoto";
        }
        #endregion
    }
}
