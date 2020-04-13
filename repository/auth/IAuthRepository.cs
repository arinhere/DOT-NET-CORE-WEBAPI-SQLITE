using System.Threading.Tasks;
using DotNetCore.API.Models;

namespace DotNetCore.API.repository.auth
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string userName, string password);
        Task<bool> UserExists(string userName);
    }
}