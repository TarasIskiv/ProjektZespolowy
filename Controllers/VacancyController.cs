using FindJobWebApi.DTOs;
using FindJobWebApi.JWTLogic;
using FindJobWebApi.Response;
using FindJobWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FindJobWebApi.Controllers
{
    [ApiController]
    [Route("api/job")]
    public class VacancyController : ControllerBase
    {
        private readonly IVacancyService _service;
        private readonly ITokenService _tokenService;

        private readonly ICookieService _cookieService;

        public VacancyController(IVacancyService service, ITokenService tokenService, ICookieService cookieService)
        {
            _service = service;
            _tokenService = tokenService;
            _cookieService = cookieService;
        }

        [Authorize(Roles = "Company")]
        [HttpPost("add")]
        public async Task<ActionResult<string>> AddNewVacancy([FromBody]CreateVacancyDTO vacancyDTO)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();

                return BadRequest(ResponseConvertor.GetResult("error", errors));
            }

            var currentCompany = User.Identity;
            var currentCompanyId = Int32.MinValue;

            if (currentCompany == null || !Int32.TryParse(currentCompany.Name, out currentCompanyId))
                return NotFound(ResponseConvertor.GetResult("error", "Problem occured by token"));

            var result = _service.AddNewVacancy(currentCompanyId, vacancyDTO);

            if (!result.Equals("OK")) 
                return Conflict(ResponseConvertor.GetResult("error", result));

            return Ok(ResponseConvertor.GetResult("OK", result));
        }

        [Authorize(Roles = "Company")]
        [HttpPost("modify")]
        public async Task<ActionResult<string>> ModifyVacancy([FromBody] ModifyVacancyDTO vacancyDTO)
        {
            var currentCompany = User.Identity;
            var currentCompanyId = Int32.MinValue;

            if (currentCompany == null || !Int32.TryParse(currentCompany.Name, out currentCompanyId))
                return NotFound(ResponseConvertor.GetResult("error", "Problem occured by token"));

            var result = _service.ModifyVacancy(currentCompanyId, vacancyDTO);

            if (result.Equals("Error")) 
                return NotFound(ResponseConvertor.GetResult("error", "Problem occured by company ID"));
            else if (result.Equals("Not yours"))
                return NotFound(ResponseConvertor.GetResult("error", "Problem occured by vacancy owner"));

            return Ok(ResponseConvertor.GetResult("OK", result));
        }

        [Authorize(Roles = "Company")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteVacancy([FromRoute] int id)
        {
            var currentCompany = User.Identity;
            var currentCompanyId = Int32.MinValue;

            if (currentCompany == null || !Int32.TryParse(currentCompany.Name, out currentCompanyId))
                return NotFound(ResponseConvertor.GetResult("error", "Problem occured by token"));

            var result = _service.DeleteVacancy(currentCompanyId, id);

            if (result.Equals("Error"))
                return NotFound(ResponseConvertor.GetResult("error", "Problem occured by company ID"));
            else if (result.Equals("Not yours"))
                return NotFound(ResponseConvertor.GetResult("error", "Problem occured by vacancy owner"));

            return Ok(ResponseConvertor.GetResult("OK", result));
        }

        #region Get Vacancies
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetJobById([FromRoute] int id)
        {
            var vacancy = _service.GetVacancyById(id);

            if (vacancy == null)
                return NotFound(ResponseConvertor.GetResult("error", "Problem occured by vacancy ID"));

            return Ok(ResponseConvertor.GetResult("OK", vacancy));
        }

        [AllowAnonymous]
        [HttpGet("search")]
        public async Task<ActionResult<List<VacancyDTO>>> SearchJob(int? minSalary, string? country, string? city, string? search)
        {
            var vacancies = _service.GetVacanciesByFilters(minSalary, country, city, search);

            if (vacancies == null)
                return NotFound(ResponseConvertor.GetResult("error", "Not found vacaniceis by selected filters"));

            return Ok(ResponseConvertor.GetResult("OK", vacancies));
        }

        #endregion

        #region InProgress
        [Authorize(Roles = "User")]
        [HttpGet("{id}/apply")]
        public async Task<ActionResult<string>> ApplyJob([FromRoute] int id)
        {
            var currentUser = User.Identity;
            var currentId = Int32.MinValue;

            if (currentUser == null || !Int32.TryParse(currentUser.Name, out currentId))
                return NotFound(ResponseConvertor.GetResult("error", "Problem occured by user token"));

            SubcribeCandidateDTO DTO = new SubcribeCandidateDTO();
            DTO.UserId = currentId;
            DTO.VacancyId = id;

            var result = _service.SubscribeVacancy(DTO);

            if (!result.Equals("OK"))
                return Conflict(ResponseConvertor.GetResult("error", result));

            return Ok(ResponseConvertor.GetResult("OK", result));
        }

        [AllowAnonymous]
        [HttpGet("{id}/candidates")]
        public async Task<ActionResult<string>> GetCandidates([FromRoute] int id)
        {
            List<UserDTO> candidates;

            var result = _service.GetCandidates(id, out candidates);

            if(result.Equals("OK"))
                return Ok(ResponseConvertor.GetResult("OK", candidates));

            return NotFound(ResponseConvertor.GetResult("error", result));
        }
        #endregion


    }
}
