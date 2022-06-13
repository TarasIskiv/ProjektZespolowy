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
        private const string CV_TEMPLATE = "<!DOCTYPE html PUBLIC \" -//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\"> <html> <head> <title></title> <meta http-equiv=\"content-type\" content=\"text/html; charset=utf-8\" /> <meta name=\"keywords\" content=\"\" /> <meta name=\"description\" content=\"\" /> <link rel=\"stylesheet\" type=\"text/css\" href=\"http://yui.yahooapis.com/2.7.0/build/reset-fonts-grids/reset-fonts-grids.css\" media=\"all\" /> <style> /* --------------------------------------------------------------------------------- STRIPPED DOWN RESUME TEMPLATE html resume v0.9: 5/28/09 design and code by: thingsthatarebrown.com (matt brown) --------------------------------------------------------------------------------- */ .msg { padding: 10px; background: #222; position: relative; } .msg h1 { color: #fff;  } .msg a { margin-left: 20px; background: #408814; color: white; padding: 4px 8px; text-decoration: none; } .msg a:hover { background: #266400; } /* //-- yui-grids style overrides -- */ body { font-family: Georgia; color: #444; } #inner { padding: 10px 80px; margin: 80px auto; background: #f5f5f5; border: solid #666; border-width: 8px 0 2px 0; } .yui-gf { margin-bottom: 2em; padding-bottom: 2em; border-bottom: 1px solid #ccc; } /* //-- header, body, footer -- */ #hd { margin: 2.5em 0 3em 0; padding-bottom: 1.5em; border-bottom: 1px solid #ccc } #hd h2 { text-transform: uppercase; letter-spacing: 2px; } #bd, #ft { margin-bottom: 2em; } /* //-- footer -- */ #ft { padding: 1em 0 5em 0; font-size: 92%; border-top: 1px solid #ccc; text-align: center; } #ft p { margin-bottom: 0; text-align: center;   } /* //-- core typography and style -- */ #hd h1 { font-size: 48px; text-transform: uppercase; letter-spacing: 3px; } h2 { font-size: 152% } h3, h4 { font-size: 122%; } h1, h2, h3, h4 { color: #333; } p { font-size: 100%; line-height: 18px; padding-right: 3em; } a { color: #990003 } a:hover { text-decoration: none; } strong { font-weight: bold; } li { line-height: 24px; border-bottom: 1px solid #ccc; } p.enlarge { font-size: 144%; padding-right: 6.5em; line-height: 24px; } p.enlarge span { color: #000 } .contact-info { margin-top: 7px; } .first h2 { font-style: italic; } .last { border-bottom: 0 } /* //-- section styles -- */ a#pdf { display: block; float: left; background: #666; color: white; padding: 6px 50px 6px 12px; margin-bottom: 6px; text-decoration: none;  } a#pdf:hover { background: #222; } .job { position: relative; margin-bottom: 1em; padding-bottom: 1em; border-bottom: 1px solid #ccc; } .job h4 { position: absolute; top: 0.35em; right: 0 } .job p { margin: 0.75em 0 3em 0; } .last { border: none; } .skills-list {  } .skills-list ul { margin: 0; } .skills-list li { margin: 3px 0; padding: 3px 0; } .skills-list li span { font-size: 152%; display: block; margin-bottom: -2px; padding: 0 } .talent { width: 32%; float: left } .talent h2 { margin-bottom: 6px; } #srt-ttab { margin-bottom: 100px; text-align: center;  } #srt-ttab img.last { margin-top: 20px } /* --// override to force 1/8th width grids -- */ .yui-gf .yui-u{width:80.2%;} .yui-gf div.first{width:12.3%;} </style> </head> <body> <div id=\"doc2\" class=\"yui-t7\"> <div id=\"inner\"> <div id=\"hd\"> <div class=\"yui-gc\"> <div class=\"yui-u first\"> <h1>[[FIRST_NAME]] [[SECOND_NAME]]</h1> <h2>[[LOCATION]]</h2> </div> <div class=\"yui-u\"> <div class=\"contact-info\"> <h3><a href=\"mailto:[[EMAIL]]\">[[EMAIL]]</a></h3> <h3>[[PHONE]]</h3> </div><!--// .contact-info --> </div> </div><!--// .yui-gc --> </div><!--// hd --> <div id=\"bd\"> <div id=\"yui-main\"> <div class=\"yui-b\"> <div class=\"yui-gf\"> <div class=\"yui-u first\"> <h2>Biography</h2> </div> <div class=\"yui-u\"> <p class=\"enlarge\"> [[BIO]] </p> </div> </div><!--// .yui-gf --> </div><!--// .yui-b --> </div><!--// yui-main --> </div><!--// bd --> <div id=\"ft\"> <p>[[FIRST_NAME]] [[SECOND_NAME]] &mdash; <a href=\"mailto:[[EMAIL]]\">[[EMAIL]]</a> &mdash; [[PHONE]]</p> </div><!--// footer --> </div><!-- // inner --> </div><!--// doc --> </body> </html>";

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
            var users = _service.GetUsersByFilters(country, city, gender, experience, search);

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
