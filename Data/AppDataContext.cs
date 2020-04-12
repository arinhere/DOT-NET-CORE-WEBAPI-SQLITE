using System.Diagnostics.CodeAnalysis;
using DotNetCore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetCore.API.Data
{
    public class AppDataContext : DbContext
    {
        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options){
        }

        public DbSet<Value> Values { get; set; }
        public DbSet<User> Users { get; set; }
    }
}