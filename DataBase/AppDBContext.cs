using FindJobWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FindJobWebApi.DataBase
{
    public class AppDBContext : DbContext
    {
        public DbSet<Candidtate> Candidtates { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }
    }
}
