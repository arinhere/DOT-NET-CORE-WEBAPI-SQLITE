using System.Diagnostics.CodeAnalysis;
using DOT_NET_CORE_WEBAPI_SQLITE.Models;
using Microsoft.EntityFrameworkCore;

namespace DOT_NET_CORE_WEBAPI_SQLITE.Data
{
    public class AppDataContext : DbContext
    {
        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options){
        }

        public DbSet<User> Users { get; set; }
    }
}