using Microsoft.EntityFrameworkCore;

namespace QuarterlySalesApp.Models
{
    public class SalesContext : DbContext
    {
        public SalesContext(DbContextOptions<SalesContext> options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Sales> Sales { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    EmployeeId = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    DOB = new DateTime(1956, 12, 15),
                    DateOfHire = new DateTime(1995, 3, 1),
                    ManagerId = 0
                },
                new Employee
                {
                    EmployeeId = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    DOB = new DateTime(1985, 7, 20),
                    DateOfHire = new DateTime(2012, 6, 15),
                    ManagerId = 1
                },
                new Employee
                {
                    EmployeeId = 3,
                    FirstName = "Michael",
                    LastName = "Johnson",
                    DOB = new DateTime(1990, 11, 5),
                    DateOfHire = new DateTime(2018, 9, 10),
                    ManagerId = 1
                }
            );

            modelBuilder.Entity<Sales>().HasData(
                new Sales
                {
                    SalesId = 1,
                    Quarter = 3,
                    Year = 2023,
                    Amount = 15000.00,
                    EmployeeId = 2
                },
                new Sales
                {
                    SalesId = 2,
                    Quarter = 4,
                    Year = 2023,
                    Amount = 20000.00,
                    EmployeeId = 1
                },
                new Sales
                {
                    SalesId = 3,
                    Quarter = 1,
                    Year = 2023,
                    Amount = 18000.00,
                    EmployeeId = 3
                },
                new Sales
                {
                    SalesId = 4,
                    Quarter = 2,
                    Year = 2023,
                    Amount = 22000.00,
                    EmployeeId = 3
                }
            );
        }
    }
}
