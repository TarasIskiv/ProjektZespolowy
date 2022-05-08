using FindJobWebApi.DTOs;
using FindJobWebApi.JWTLogic;
using FindJobWebApi.Response;
using FindJobWebApi.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FindJobWebApi.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly ITokenService _tokenService;

        private readonly ICookieService _cookieService;

        public UserController(IUserService service, ITokenService tokenService, ICookieService cookieService)
        {
            _service = service;
            _tokenService = tokenService;
            _cookieService = cookieService;
        }

        #region Sign In
        [AllowAnonymous]
        [HttpPost("signin")]
        public async Task<ActionResult<string>> Signin([FromBody] LoginUserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();
                return BadRequest(ResponseConvertor.GetResult("error", errors));
            }
            var result = _service.SignIn(userDTO);


            if (!int.TryParse(result, out int id))
            {
                return BadRequest(ResponseConvertor.GetResult("error", result));
            }

            var token = _tokenService.generateToken(id, "User");

            var claimsPrincipal = _cookieService.GetClaimsPrincipal("User", token);
            await HttpContext.SignInAsync("Cookie", claimsPrincipal);

            return Ok(ResponseConvertor.GetResult("OK", token));    
        }
        #endregion

        #region Sign Up
        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<ActionResult<string>> SignUp([FromBody] CreateUserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();

                return BadRequest(ResponseConvertor.GetResult("error", errors));
            }

            var result = _service.SignUp(userDTO);

            if (!result.Equals("OK")) return Conflict(ResponseConvertor.GetResult("error", result));

            return Ok(ResponseConvertor.GetResult("OK", result));
        }
        #endregion

        #region Get Users
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetUserById([FromRoute] int id)
        {
            return $"GetUserById : {id}";
        }
        [HttpGet("list")]
        public async Task<ActionResult<string>> GetUsers()
        {
            return "User List";
        }
        #endregion

        #region Get Profile
        [Authorize(Roles="User")]
        [HttpGet("profile")]
        public async Task<ActionResult<string>> GetUserProfile()
        {
            var currentUser = User.Identity;
            var currentId = Int32.MinValue;

            if (currentUser == null || !Int32.TryParse(currentUser.Name, out currentId))
                return NotFound(ResponseConvertor.GetResult("error", "Problem occured by token"));

            var user = _service.GetUser(currentId);

            if(user == null)
                return NotFound(ResponseConvertor.GetResult("error", "Problem occured by user ID"));

            return Ok(ResponseConvertor.GetResult("OK", user));
        }
        #endregion

        #region Modify Profile
        [Authorize(Roles = "User")]
        [HttpPost("profile")]
        public async Task<ActionResult<string>> ModifyUserProfile(ModifyUserDTO dto)
        {
            var currentUser = User.Identity;
            var currentId = Int32.MinValue;

            if (currentUser == null || !Int32.TryParse(currentUser.Name, out currentId))
                return NotFound(ResponseConvertor.GetResult("error", "Problem occured by token"));

            var result = _service.AddProfile(currentId, dto);

            if (result.Equals("Error")) 
                return NotFound(ResponseConvertor.GetResult("error", "Problem occured by company ID"));

            return Ok(ResponseConvertor.GetResult("OK", result));

        }
        #endregion

        #region CV
        [HttpGet("profile/cv/create")]
        public async Task<ActionResult<string>> CreateCVForUser()
        {
            return "CreateCVForUser";
        }
        [HttpPut("profile/upload")]
        public async Task<ActionResult<string>> UploadUserProfile()
        {
            return "UploadUserProfile";
        }
        #endregion
    }
}
