using System.Threading.Tasks;
using DotNetCore.API.Models;

namespace DotNetCore.API.repository.user
{
    public interface IUserRepository
    {
        Task<User> GetUser(int id);
    }
}