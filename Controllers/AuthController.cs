using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DotNetCore.API.Data;
using DotNetCore.API.DTO;
using DotNetCore.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DotNetCore.API.Controllers
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
        public async Task<IActionResult> Register(userDto dtoInstance) // DTO needs to be used, because user will send data as an object from Angular.
        {
            if (!await _auth.UserExists(dtoInstance.userName))
                return BadRequest("User already exists");

            var user = new User { UserName = dtoInstance.userName };
            await _auth.Register(user, dtoInstance.password);

            return StatusCode(201);
        }

        // Post api/auth/login
        [HttpPost("login")]
        public async Task<ActionResult> Login(userDto dtoInstance)
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