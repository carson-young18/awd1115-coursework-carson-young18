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
                .HasKey(bi => new {bi.BuildId, bi.ItemId});

            builder.Entity<BuildItem>()
                .HasOne(bi => bi.Build)
                .WithMany(b => b.BuildItems)
                .HasForeignKey(b => b.BuildId);

            builder.Entity<BuildItem>()
                .HasOne(bi => bi.Item)
                .WithMany(i => i.BuildItems)
                .HasForeignKey(bi => bi.ItemId);

            builder.Entity<Category>().HasData(
                    new Category { CategoryId = 1, Name = "Tank"},
                    new Category { CategoryId = 2, Name = "Support" },
                    new Category { CategoryId = 3, Name = "Mage" },
                    new Category { CategoryId = 4, Name = "Damage" },
                    new Category { CategoryId = 5, Name = "Ranged" },
                    new Category { CategoryId = 6, Name = "Melee" }
                );

            builder.Entity<Champion>().HasData(
                    new Champion { ChampionId = 1, Name = "Yorick"},
                    new Champion { ChampionId = 2, Name = "Thresh" },
                    new Champion { ChampionId = 3, Name = "Kindred" },
                    new Champion { ChampionId = 4, Name = "Brand" },
                    new Champion { ChampionId = 5, Name = "Aatrox" }
                );

            builder.Entity<Item>().HasData(
                    new Item { ItemId = 1, Name = "Hullbreaker", Cost = 3000 },
                    new Item { ItemId = 2, Name = "Knight's Vow", Cost = 2300 },
                    new Item { ItemId = 3, Name = "Rapid Firecannon", Cost = 2650 },
                    new Item { ItemId = 4, Name = "Rabadon's Deathcap", Cost = 3500 },
                    new Item { ItemId = 5, Name = "Bloodthirster", Cost = 3400 },
                    new Item { ItemId = 6, Name = "Spirit Visage", Cost = 2700 }
                );

            builder.Entity<Build>().HasData(
                    new Build 
                    { 
                        BuildId = 1, 
                        UserId = "8ba42c86-75f2-43b3-8efa-13b2f3115e0e", 
                        CategoryId = 1, 
                        ChampionId = 1,
                        TotalCost = 24000,
                        Name = "Yorick Top"
                    }
                );

            builder.Entity<BuildItem>().HasData(
                    new BuildItem { BuildId = 1, ItemId = 1 },
                    new BuildItem { BuildId = 1, ItemId = 2 },
                    new BuildItem { BuildId = 1, ItemId = 3 },
                    new BuildItem { BuildId = 1, ItemId = 4 },
                    new BuildItem { BuildId = 1, ItemId = 5 },
                    new BuildItem { BuildId = 1, ItemId = 6 }
                );
        }
    }
}
