using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data
{
    public class AppDbContext : IdentityDbContext<Teacher>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { } 

        public DbSet<Student> Students { get; set; }
        public DbSet<School> Schools {  get; set; }
        public DbSet<SchoolYear> SchoolYears { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<School>().HasData(
                new School { Id = 1, Name = "Telebachillerato Coacotla", Code = "30ETH0224D" },
                new School { Id = 2, Name = "Telebachillerato Nopalapan", Code = "30ETH0206O" },
                new School { Id = 3, Name = "Telebachillerato Lealtad de Muñoz", Code = "30ETH0472L" },
                new School { Id = 4, Name = "Telebachillerato Isla", Code = "30ETH0134L" }
            );

            builder.Entity<SchoolYear>().HasData(
                new SchoolYear { Id = 1, Name = "2024-2025", StartDate = new DateTime(2024, 9, 1), EndDate = new DateTime(2025, 6, 30), IsActive = true }
            );

        }
    }
}
