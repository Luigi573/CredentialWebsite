using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data
{
    public class AppDbContext : IdentityDbContext<Teacher>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { } 

        public DbSet<Student> Students { get; set; }
        public DbSet<School> Schools {  get; set; }
    }
}
