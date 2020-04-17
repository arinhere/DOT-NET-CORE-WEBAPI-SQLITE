using System.Threading.Tasks;
using AutoMapper;
using DOT_NET_CORE_WEBAPI_SQLITE.DTO.users;
using DOT_NET_CORE_WEBAPI_SQLITE.Repository.user;
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
        private readonly IMapper _mapper;
        public UserController(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET api/user/get/id
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUser(id);
            if (user == null)
                return NotFound("Invalid user id provided");

            var userToReturn = _mapper.Map<UserResponseDto>(user);
            return Ok(userToReturn);
        }

        // Put api/user/update
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser(UpdateUserDto dtoInstance)
        {
            var user = await _repo.UpdateUser(dtoInstance);
            if (user == null)
                return NotFound("Invalid user id provided");

            var userToReturn = _mapper.Map<UserResponseDto>(user);
            return Ok(user);
        }

    }
}