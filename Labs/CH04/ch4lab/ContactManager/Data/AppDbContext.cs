using ContactManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Contact> Contacts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed categories
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Family" },
                new Category { CategoryId = 2, CategoryName = "Friend" },
                new Category { CategoryId = 3, CategoryName = "Work" },
                new Category { CategoryId = 4, CategoryName = "Other" }
            );

            // Seed one contact (must supply CategoryId that exists)
            modelBuilder.Entity<Contact>().HasData(
                new Contact
                {
                    ContactId = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Phone = "555-123-4567",
                    Email = "john.doe@example.com",
                    CategoryId = 3,
                    DateAdded = new DateTime(2025, 1, 1) // use a fixed date for seeding
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}