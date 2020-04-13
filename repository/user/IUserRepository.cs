using System.Threading.Tasks;
using DOT_NET_CORE_WEBAPI_SQLITE.DTO.users;
using DOT_NET_CORE_WEBAPI_SQLITE.Models;

namespace DOT_NET_CORE_WEBAPI_SQLITE.repository.user
{
    public interface IUserRepository
    {
        Task<User> GetUser(int id);
        Task<User> UpdateUser(updateUserDto user);
    }
}