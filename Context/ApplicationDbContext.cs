using EfCoreDockerCompose.Models;
using Microsoft.EntityFrameworkCore;

namespace EfCoreDockerCompose.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
    }
}
