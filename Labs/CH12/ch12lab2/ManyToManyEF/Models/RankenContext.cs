using Microsoft.EntityFrameworkCore;

namespace ManyToManyEF.Models
{
    public class RankenContext : DbContext
    {
        public RankenContext(DbContextOptions<RankenContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
                new Student { StudentId = 1, Name = "Alice Johnson", FinancialAidStatus = "Passed" },
                new Student { StudentId = 2, Name = "Bob Smith", FinancialAidStatus = "Passed" },
                new Student { StudentId = 3, Name = "Charlie Brown", FinancialAidStatus = "Passed" },
                new Student { StudentId = 4, Name = "Diana Prince", FinancialAidStatus = "Passed" }
                );

            modelBuilder.Entity<Course>().HasData(
                new Course { CourseId = 1, Title = "Introduction to Programming" },
                new Course { CourseId = 2, Title = "Database Systems" },
                new Course { CourseId = 3, Title = "Web Development" },
                new Course { CourseId = 4, Title = "Data Structures and Algorithms" }
                );

            modelBuilder.Entity<Student>().HasMany(s => s.Courses).WithMany(c => c.Students).UsingEntity(sc => sc.HasData(
                new { StudentsStudentId = 1, CoursesCourseId = 1 },
                new { StudentsStudentId = 2, CoursesCourseId = 1 },
                new { StudentsStudentId = 3, CoursesCourseId = 1 },
                new { StudentsStudentId = 4, CoursesCourseId = 1 },
                new { StudentsStudentId = 1, CoursesCourseId = 2 },
                new { StudentsStudentId = 2, CoursesCourseId = 2 },
                new { StudentsStudentId = 3, CoursesCourseId = 3 }
                ));
        }
    }
}
