using System.Threading.Tasks;
using DotNetCore.API.repository.user;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCore.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repo;
        public UserController(IUserRepository repo)
        {
            _repo = repo;
        }

        // GET api/user/get/id
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetUser(int id){
            var user = await _repo.GetUser(id);
            if(user == null)
                return NotFound("Invalid user id provided");

            return Ok(user);
        }
    }
}