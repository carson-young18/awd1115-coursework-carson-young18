using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RankenClassSchedule.Models.DomainModels;


namespace RankenClassSchedule.Models.Configuration
{
    public class ClassesConfig : IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> entity)
        {
            entity.HasOne(c => c.Teacher)
                  .WithMany(t => t.Classes)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasData(
                new Class { ClassId = 1, Title = "Introduction to Programming", Number = 101, TeacherId = 1, DayId = 1, MilitaryTime = "0900" },
                new Class { ClassId = 2, Title = "Data Structures", Number = 201, TeacherId = 2, DayId = 2, MilitaryTime = "1100" },
                new Class { ClassId = 3, Title = "Database Systems", Number = 301, TeacherId = 3, DayId = 3, MilitaryTime = "1300" },
                new Class { ClassId = 4, Title = "Web Development", Number = 401, TeacherId = 4, DayId = 4, MilitaryTime = "1500" },
                new Class { ClassId = 5, Title = "Software Engineering", Number = 501, TeacherId = 5, DayId = 5, MilitaryTime = "1700" },
                new Class { ClassId = 6, Title = "Network Fundamentals", Number = 150, TeacherId = 1, DayId = 2, MilitaryTime = "1000" },
                new Class { ClassId = 7, Title = "Operating Systems", Number = 250, TeacherId = 2, DayId = 3, MilitaryTime = "1200" },
                new Class { ClassId = 8, Title = "Cybersecurity Basics", Number = 350, TeacherId = 3, DayId = 4, MilitaryTime = "1400" }
                );
        }
    }
}
