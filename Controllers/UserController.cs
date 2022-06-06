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
        private const string CV_TEMPLATE = "<!DOCTYPE html> <html lang=\"en\"> <head> <meta charset=\"UTF-8\"> <title>Document</title> <style> .container { padding: 20px 30px; } .container > div > p { margin-top: -15px; margin-bottom: 30px; } </style> </head> <body> <div class=\"container\"> <h1>[[FIRST_NAME]] [[SECOND_NAME]]</h1> <div> <h3>Location</h3> <p>[[LOCATION]]</p> </div> <div> <h3>Biography</h3> <p>[[BIO]]</p> </div> <div> <h3>Contact</h3> <p>Email: [[EMAIL]]</p> <p>Phone: [[PHONE]]</p> </div> </div> </body> </html>";
        
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
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetUserById([FromRoute] int id)
        {
            var user = _service.GetUser(id);

            if (user == null)
                return NotFound(ResponseConvertor.GetResult("error", "Problem occured by user ID"));

            return Ok(ResponseConvertor.GetResult("OK", user));
        }

        [AllowAnonymous]
        [HttpGet("list")]
        public async Task<ActionResult<string>> GetUsers()
        {
            var users = _service.GetUsers();
            if (users == null) return BadRequest(ResponseConvertor.GetResult("error", "There aren't found users"));

            return Ok(ResponseConvertor.GetResult("OK", users));
        }

        [AllowAnonymous]
        [HttpGet("search")]
        public async Task<ActionResult<int>> SearchUser(string? country, string? city, string? gender, float? experience, string? search)
        {
            if (experience == null || experience == default(float))
                experience = 0;

            var users = _service.GetUsersByFilters(country, city, gender, (float)experience, search);

            if (users == null)
                return NotFound(ResponseConvertor.GetResult("error", "Not found users by selected filters"));

            return Ok(ResponseConvertor.GetResult("OK", users));
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
        [AllowAnonymous]
        [HttpPost("cv")]
        public async Task<ActionResult<string>> CreateCVForUser([FromBody] CreateCVDTO createCVDTO)
        {
            HtmlLoadOptions options = new HtmlLoadOptions();
            Document pdfDocument = new Document(GenerateStreamFromString(CV_TEMPLATE), options);
            //Document pdfDocument = new Document($"CV Templates\\1.html", options);

            /*
            try
            {
                pdfDocument = new Document($"CV Templates\\{createCVDTO.Template}.html", options);
            }
            catch
            {
                pdfDocument = new Document($"CV Templates\\1.html", options);
            }*/

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

        private Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        [HttpPut("profile/upload")]
        public async Task<ActionResult<string>> UploadUserProfile()
        {
            return "UploadUserProfile";
        }
        #endregion
    }
}
