using dotnet_mvc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ProductAPI.Models;


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
            SeedProducts(builder);

            builder.Entity<Product>()
               .HasIndex(p => p.ProductName);
        }
        public DbSet<Product> Products { get; set; }

        private static void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData
                (
                new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "ADMIN" },
                new IdentityRole() { Name = "User", ConcurrencyStamp = "2", NormalizedName = "USER" }
                );
        }

        private static void SeedProducts(ModelBuilder builder)
        {
            builder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = Guid.NewGuid().ToString(),
                    ProductName = "Wireless Mouse",
                    Slug = "wireless-mouse",
                    ProductImage = "mouse.jpg",
                    Price = 25.99m,
                    DiscountStart = DateTime.UtcNow,
                    DiscountEnd = DateTime.UtcNow.AddDays(7)
                },
                new Product
                {
                    ProductId = Guid.NewGuid().ToString(),
                    ProductName = "Mechanical Keyboard",
                    Slug = "mechanical-keyboard",
                    ProductImage = "keyboard.jpg",
                    Price = 79.99m,
                    DiscountStart = DateTime.UtcNow,
                    DiscountEnd = DateTime.UtcNow.AddDays(7)
                },
                new Product
                {
                    ProductId = Guid.NewGuid().ToString(),
                    ProductName = "HD Monitor",
                    Slug = "hd-monitor",
                    ProductImage = "monitor.jpg",
                    Price = 199.99m,
                    DiscountStart = DateTime.UtcNow,
                    DiscountEnd = DateTime.UtcNow.AddDays(10)
                }
            );
        }

    }
}