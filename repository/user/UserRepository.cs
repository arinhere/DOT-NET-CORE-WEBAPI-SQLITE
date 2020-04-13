using System.Threading.Tasks;
using DotNetCore.API.Data;
using DotNetCore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetCore.API.repository.user
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDataContext _data;
        public UserRepository(AppDataContext data)
        {
            _data = data;
        }
        public async Task<User> GetUser(int id)
        {
            User user = await _data.Users.FirstOrDefaultAsync(x => x.Id == id);
            if(user == null)
                return null;
            
            return user;
        }
    }
}