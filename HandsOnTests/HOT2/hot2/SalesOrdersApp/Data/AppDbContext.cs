using Microsoft.EntityFrameworkCore;
using SalesOrdersApp.Models;

namespace SalesOrdersApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryID);


            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryID = 1, CategoryName = "Accessories" },
                new Category { CategoryID = 2, CategoryName = "Bikes" },
                new Category { CategoryID = 3, CategoryName = "Clothing" },
                new Category { CategoryID = 4, CategoryName = "Components" },
                new Category { CategoryID = 5, CategoryName = "Car racks" },
                new Category { CategoryID = 6, CategoryName = "Wheels" }
            );


            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductID = 1,
                    ProductName = "AeroFlo ATB Wheels",
                    ProductDescShort = "",
                    ProductDescLong = "",
                    ProductImage = "",
                    ProductPrice = 189,
                    ProductQty = 40,
                    CategoryID = 4
                },
                new Product
                {
                    ProductID = 2,
                    ProductName = "Clear Shade 85-T Glasses",
                    ProductDescShort = "",
                    ProductDescLong = "",
                    ProductImage = "",
                    ProductPrice = 45,
                    ProductQty = 14,
                    CategoryID = 1
                },
                new Product
                {
                    ProductID = 3,
                    ProductName = "Cosmic Elite Road Warrior Wheels",
                    ProductDescShort = "",
                    ProductDescLong = "",
                    ProductImage = "",
                    ProductPrice = 165,
                    ProductQty = 22,
                    CategoryID = 4
                },
                new Product
                {
                    ProductID = 4,
                    ProductName = "Cycle-Doc Pro Repair Stand",
                    ProductDescShort = "",
                    ProductDescLong = "",
                    ProductImage = "",
                    ProductPrice = 166,
                    ProductQty = 12,
                    CategoryID = 1
                },
                new Product
                {
                    ProductID = 5,
                    ProductName = "Dog Ear Aero-Flow Floor Pump",
                    ProductDescShort = "",
                    ProductDescLong = "",
                    ProductImage = "",
                    ProductPrice = 5,
                    ProductQty = 25,
                    CategoryID = 1
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}