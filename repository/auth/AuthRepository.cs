using System;
using System.Threading.Tasks;
using DOT_NET_CORE_WEBAPI_SQLITE.Data;
using DOT_NET_CORE_WEBAPI_SQLITE.Models;
using Microsoft.EntityFrameworkCore;

namespace DOT_NET_CORE_WEBAPI_SQLITE.Repository.auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDataContext _context;
        public AuthRepository(AppDataContext context)
        {
            _context = context;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            createPasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> Login(string userName, string password)
        {
            User userData = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            if(!verifyPasswordHash(password, userData.PasswordHash, userData.PasswordSalt))
                return null;
            
            return userData;
        }

        public async Task<bool> UserExists(string userName)
        {
            if(await _context.Users.AnyAsync(x => x.UserName == userName)) // AnyAsync returns boolean          
                return false;

            return true;
        }

        private bool verifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)){
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for(int i =0;i< computedHash.Length; i++){
                    if(computedHash[i]!=passwordHash[i])
                        return false;
                }
            }

            return true;
        }

        private void createPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512()){
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}