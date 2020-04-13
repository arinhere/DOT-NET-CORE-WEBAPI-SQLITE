using System.Threading.Tasks;
using DOT_NET_CORE_WEBAPI_SQLITE.DTO.users;
using DOT_NET_CORE_WEBAPI_SQLITE.Models;
using DOT_NET_CORE_WEBAPI_SQLITE.repository.user;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DOT_NET_CORE_WEBAPI_SQLITE.Controllers
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

        // Put api/user/update
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser(updateUserDto dtoInstance){
            var user = await _repo.UpdateUser(dtoInstance);
            if(user == null)
                return NotFound("Invalid user id provided");

            return Ok(user);
        }

    }
}