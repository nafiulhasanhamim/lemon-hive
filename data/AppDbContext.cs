using dotnet_mvc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace dotnet_mvc.data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedRoles(builder);
        }

        private static void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData
                (
                new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "ADMIN" },
                new IdentityRole() { Name = "User", ConcurrencyStamp = "2", NormalizedName = "USER" }
                );
        }

    }
}