using System.Collections.Generic;
using System.Threading.Tasks;
using DOT_NET_CORE_WEBAPI_SQLITE.Data;
using DOT_NET_CORE_WEBAPI_SQLITE.DTO.users;
using DOT_NET_CORE_WEBAPI_SQLITE.Models;
using Microsoft.EntityFrameworkCore;

namespace DOT_NET_CORE_WEBAPI_SQLITE.Repository.user
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDataContext _data;
        public UserRepository(AppDataContext data)
        {
            _data = data;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var users = await _data.Users.FromSqlRaw<User>("sp_getUsers").ToListAsync();
            // If you want to pass any value use the following
            // _data.Users.FromSqlRaw<User>("sp_getUsers {0}", Pass_Parameter_Here).ToListAsync()
            if(users == null)
                return null;
            
            return users;
        }
        public async Task<User> GetUser(int id)
        {
            User user = await _data.Users.Include(p => p.Products).FirstOrDefaultAsync(x => x.Id == id);
            if(user == null)
                return null;
            
            return user;
        }

        public async Task<User> UpdateUser(UpdateUserDto user)
        {
            User userData = await _data.Users.FirstOrDefaultAsync(x => x.Id == user.id);
            if(userData == null)
                return null;
            
            userData.Name = user.name;
            _data.SaveChanges();

            return userData;
        }
    }
}