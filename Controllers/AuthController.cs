using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DOT_NET_CORE_WEBAPI_SQLITE.DTO.users;
using DOT_NET_CORE_WEBAPI_SQLITE.Models;
using DOT_NET_CORE_WEBAPI_SQLITE.Repository.auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DOT_NET_CORE_WEBAPI_SQLITE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _auth;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository auth, IConfiguration config)
        {
            _config = config;
            _auth = auth;
        }

        // Post api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRequestDto dtoInstance) // DTO needs to be used, because user will send data as an object from Angular.
        {
            if (!await _auth.UserExists(dtoInstance.userName))
                return BadRequest("User already exists");

            var user = new User { 
                UserName = dtoInstance.userName,
                Name = dtoInstance.name
            };
            await _auth.Register(user, dtoInstance.password);

            return StatusCode(201);
        }

        // Post api/auth/login
        [HttpPost("login")]
        public async Task<ActionResult> Login(UserRequestDto dtoInstance)
        {
            var userData = await _auth.Login(dtoInstance.userName, dtoInstance.password);
            if (userData == null)
                return Unauthorized("Invalid user credentials");

            SecurityToken token;
            var tokenHandler = new JwtSecurityTokenHandler();
            CreateJwtToken(userData, tokenHandler, out token);
            
            return Ok(new {
                token = tokenHandler.WriteToken(token)
            });

        }

        private void CreateJwtToken(User userData, JwtSecurityTokenHandler tokenHandler, out SecurityToken token)
        {
            // start creating JWT token  
            var claims = new[] // First Set paramaters of the tokens.
            {
                new Claim(ClaimTypes.NameIdentifier, userData.Id.ToString()),
                new Claim(ClaimTypes.Name, userData.UserName)
            };

            // Create Key to create signing credentials
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Appsettings:TokenSecret").Value));

            // Generate Signing Credentials
            var signInCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            // Create Descriptor, which is a combination of the above
            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = System.DateTime.Now.AddHours(1),
                SigningCredentials = signInCred
            };

            // return generated token
            token = tokenHandler.CreateToken(tokenDescriptor);
        }
    }
}