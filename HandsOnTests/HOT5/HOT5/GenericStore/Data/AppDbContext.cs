using Microsoft.EntityFrameworkCore;
using GenericStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GenericStore.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Electronics" },
                new Category { CategoryId = 2, Name = "Home" },
                new Category { CategoryId = 3, Name = "Toys" },
                new Category { CategoryId = 4, Name = "Outdoors" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, Name = "Bluetooth Headphones", Brand = "AcoustiX", Color = "Black", StockQty = 20, Price = 49.99m, ImageFileName = "headphones.jpg", Slug = "bluetooth-headphones", CategoryId = 1 },
                new Product { ProductId = 2, Name = "Wireless Mouse", Brand = "ClickPro", Color = "White", StockQty = 50, Price = 19.99m, ImageFileName = "mouse.jpg", Slug = "wireless-mouse", CategoryId = 1 },
                new Product { ProductId = 3, Name = "LED Desk Lamp", Brand = "BrightLite", Color = "Black", StockQty = 30, Price = 29.99m, ImageFileName = "lamp.jpg", Slug = "led-desk-lamp", CategoryId = 2 },
                new Product { ProductId = 4, Name = "Ceramic Mug", Brand = "HomeBrew", Color = "Blue", StockQty = 100, Price = 9.99m, ImageFileName = "mug.jpg", Slug = "ceramic-mug", CategoryId = 2 },
                new Product { ProductId = 5, Name = "Action Figure - Hero", Brand = "PlayTime", Color = "Multicolor", StockQty = 40, Price = 14.99m, ImageFileName = "figure1.jpg", Slug = "action-figure-hero", CategoryId = 3 },
                new Product { ProductId = 6, Name = "Action Figure - Villain", Brand = "PlayTime", Color = "Red", StockQty = 35, Price = 14.99m, ImageFileName = "figure2.jpg", Slug = "action-figure-villain", CategoryId = 3 },
                new Product { ProductId = 7, Name = "Camping Tent 2P", Brand = "TrailMate", Color = "Green", StockQty = 15, Price = 89.99m, ImageFileName = "tent.jpg", Slug = "camping-tent-2p", CategoryId = 4 },
                new Product { ProductId = 8, Name = "Water Bottle 1L", Brand = "HydroMax", Color = "Silver", StockQty = 120, Price = 12.99m, ImageFileName = "bottle.jpg", Slug = "water-bottle-1l", CategoryId = 4 },
                new Product { ProductId = 9, Name = "Bluetooth Speaker", Brand = "AcoustiX", Color = "Black", StockQty = 25, Price = 39.99m, ImageFileName = "speaker.jpg", Slug = "bluetooth-speaker", CategoryId = 1 },
                new Product { ProductId = 10, Name = "Throw Pillow", Brand = "HomeBrew", Color = "Gray", StockQty = 60, Price = 24.99m, ImageFileName = "pillow.jpg", Slug = "throw-pillow", CategoryId = 2 }
            );
        }
    }
}
