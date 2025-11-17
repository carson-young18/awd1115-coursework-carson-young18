using Microsoft.EntityFrameworkCore;
using TripLog2.Models.DataAccess.Configuration;
using TripLog2.Models.DomainModels;

namespace TripLog2.Models.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Trip> Trips { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TripConfig());
        }
    }
}
