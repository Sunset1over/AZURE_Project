using API.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.DAL.EF
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Animal> Animals => Set<Animal>();
        public DbSet<Disease> Diseases => Set<Disease>();
        public DbSet<Measurement> Measurements => Set<Measurement>();
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }
    }
}
