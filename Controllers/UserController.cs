using Aspose.Pdf;
using Aspose.Pdf.Text;
using FindJobWebApi.DTOs;
using FindJobWebApi.JWTLogic;
using FindJobWebApi.Response;
using FindJobWebApi.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        [Authorize(Roles = "User")]
        [HttpPost("cv")]
        public async Task<ActionResult<string>> CreateCVForUser([FromBody] CreateCVDTO createCVDTO)
        {
            HtmlLoadOptions options = new HtmlLoadOptions();
            Document pdfDocument;

            try
            {
                pdfDocument = new Document($"CV Templates\\{createCVDTO.Template}.html", options);
            }
            catch
            {
                pdfDocument = new Document($"CV Templates\\1.html", options);
            }

            ReplaceText(pdfDocument, "[[FIRST_NAME]]", createCVDTO.FirstName);
            ReplaceText(pdfDocument, "[[SECOND_NAME]]", createCVDTO.LastName);
            ReplaceText(pdfDocument, "[[LOCATION]]", createCVDTO.Location);
            ReplaceText(pdfDocument, "[[BIO]]", createCVDTO.Bio);
            ReplaceText(pdfDocument, "[[EMAIL]]", createCVDTO.Email);
            ReplaceText(pdfDocument, "[[PHONE]]", createCVDTO.Phone);

            MemoryStream output = new MemoryStream();
            pdfDocument.Save(output);

            output.Position = 0;

            FileStreamResult result = new FileStreamResult(output, "application/pdf");
            result.FileDownloadName = "Sample.pdf";

            return result;
        }

        private void ReplaceText(Document pdf, string replace, string to)
        {
            TextFragmentAbsorber textFragmentAbsorber = new TextFragmentAbsorber(replace);

            pdf.Pages.Accept(textFragmentAbsorber);

            TextFragmentCollection textFragments = textFragmentAbsorber.TextFragments;

            foreach (TextFragment textFragment in textFragments)
            {
                textFragment.Text = to;
            }
        }

        [HttpPut("profile/upload")]
        public async Task<ActionResult<string>> UploadUserProfile()
        {
            return "UploadUserProfile";
        }
        #endregion
    }
}
