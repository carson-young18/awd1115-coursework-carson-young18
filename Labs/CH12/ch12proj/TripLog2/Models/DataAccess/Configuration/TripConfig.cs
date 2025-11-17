using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripLog2.Models.DomainModels;

namespace TripLog2.Models.DataAccess.Configuration
{
    public class TripConfig : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> entity)
        {
            entity.HasOne(t => t.Destination)
                  .WithMany(d => d.Trips)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(t => t.Accomodation)
                  .WithMany(a => a.Trips)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
