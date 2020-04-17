using System.Threading.Tasks;
using DOT_NET_CORE_WEBAPI_SQLITE.Models;

namespace DOT_NET_CORE_WEBAPI_SQLITE.Repository.auth
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string userName, string password);
        Task<bool> UserExists(string userName);
    }
}