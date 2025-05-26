using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MyApp.Domain.Entities;


namespace MyApp.Infrastructure.Data
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

            builder.Entity<Cart>()
            .HasOne(c => c.Product)      // link the navigation property
            .WithMany()                  // Product has no navigation back to Cart
            .HasForeignKey(c => c.ProductId) // tell EF to use ProductId
            .IsRequired();

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }



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
                new Product("Wireless Mouse", "wireless-mouse", "mouse.jpg", 25.99m,
                            DateTime.UtcNow.AddDays(1),   // discount started yesterday
                            DateTime.UtcNow.AddDays(6))     // ends in 6 days
               ,
                new Product("Mechanical Keyboard", "mechanical-keyboard", "keyboard.jpg", 79.99m,
                            DateTime.UtcNow,              // discount starts now
                            DateTime.UtcNow.AddDays(7)),
                new Product("HD Monitor 27\"", "hd-monitor-27", "monitor27.jpg", 199.99m,
                            DateTime.UtcNow.AddDays(2),
                            DateTime.UtcNow.AddDays(5)),
                new Product("Gaming Chair", "gaming-chair", "chair.jpg", 139.50m,
                            DateTime.UtcNow.AddDays(1),   // discount starts tomorrow
                            DateTime.UtcNow.AddDays(10)),
                new Product("USB-C Hub (6-in-1)", "usb-c-hub-6-in-1", "hub.jpg", 45.00m,
                            DateTime.UtcNow.AddDays(3),
                            DateTime.UtcNow.AddDays(4)),
                new Product("1080p Webcam Pro", "webcam-pro-1080p", "webcam.jpg", 59.99m,
                            DateTime.UtcNow,
                            DateTime.UtcNow.AddDays(14)),
                new Product("Bluetooth Speaker", "bluetooth-speaker", "speaker.jpg", 89.95m,
                            DateTime.UtcNow.AddDays(5),
                            DateTime.UtcNow.AddDays(6)),
                new Product("External SSD 1TB", "external-ssd-1tb", "ssd.jpg", 129.99m,
                            DateTime.UtcNow.AddDays(10),
                            DateTime.UtcNow.AddDays(11)),
                new Product("Noise-Cancelling Headphones", "nc-headphones", "headphones.jpg", 159.99m,
                            DateTime.UtcNow.AddDays(1),
                            DateTime.UtcNow.AddDays(5)),
                new Product("4K Ultra HD TV", "4k-ultra-hd-tv", "tv.jpg", 799.00m,
                            DateTime.UtcNow,
                            DateTime.UtcNow.AddDays(30))
            );
        }

    }
}