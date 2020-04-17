using System.Threading.Tasks;
using DOT_NET_CORE_WEBAPI_SQLITE.DTO.users;
using DOT_NET_CORE_WEBAPI_SQLITE.Models;

namespace DOT_NET_CORE_WEBAPI_SQLITE.Repository.user
{
    public interface IUserRepository
    {
        Task<User> GetUser(int id);
        Task<User> UpdateUser(UpdateUserDto user);
    }
}