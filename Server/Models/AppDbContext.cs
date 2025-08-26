using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.Models;

namespace Server.Models
{
    public class AppDbContext : DbContext
    {
        private IConfiguration _configuration;

        public DbSet<Grade> Grades => Set<Grade>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Teacher> Teachers => Set<Teacher>();

        public AppDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Npgsql"));
            base.OnConfiguring(optionsBuilder);
        }
    }
}
