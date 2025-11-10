using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RankenClassSchedule.Models.DomainModels;

namespace RankenClassSchedule.Models.Configuration
{
    public class TeacherConfig : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> entity)
        {
            entity.HasData(
                new Teacher { TeacherId = 1, FirstName = "Evan", LastName = "Doe" },
                new Teacher { TeacherId = 2, FirstName = "Jane", LastName = "Smith" },
                new Teacher { TeacherId = 3, FirstName = "Emily", LastName = "Johnson" },
                new Teacher { TeacherId = 4, FirstName = "Michael", LastName = "Brown" },
                new Teacher { TeacherId = 5, FirstName = "Sarah", LastName = "Davis" }
                );
        }
    }
}
