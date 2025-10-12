using Microsoft.EntityFrameworkCore;
using TripPlannerApp.Models;

namespace TripPlannerApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Trip> Trips { get; set; } = null!;
    }
}