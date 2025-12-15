using FinalProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Build> Builds { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Champion> Champions { get; set; }
        public DbSet<BuildItem> BuildItems { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<BuildItem>()
                .HasKey(bi => new { bi.BuildId, bi.ItemId });

            builder.Entity<BuildItem>()
                .HasOne(bi => bi.Build)
                .WithMany(b => b.BuildItems)
                .HasForeignKey(b => b.BuildId);

            builder.Entity<BuildItem>()
                .HasOne(bi => bi.Item)
                .WithMany(i => i.BuildItems)
                .HasForeignKey(bi => bi.ItemId);

            builder.Entity<Category>().HasData(
                    new Category { CategoryId = 1, Name = "Tank" },
                    new Category { CategoryId = 2, Name = "Support" },
                    new Category { CategoryId = 3, Name = "Mage" },
                    new Category { CategoryId = 4, Name = "Damage" },
                    new Category { CategoryId = 5, Name = "Ranged" },
                    new Category { CategoryId = 6, Name = "Melee" }
                );
        }
    }
}
