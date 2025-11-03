namespace QuarterlySalesApp.Models.Validation
{
    public static class Validate
    {
        public static string CheckEmployee(SalesContext context, Employee employee)
        {
            var dbEmp = context.Employees
                .FirstOrDefault(e => e.FirstName == employee.FirstName &&
                                     e.LastName == employee.LastName &&
                                     e.DOB == employee.DOB);

            if (dbEmp == null)
            {
                return "";
            }
            else
            {
                return ($"{employee.FirstName} (DOB:{employee.DOB?.ToShortDateString()}) is already in the database");
            }
        }

        public static string CheckManagerEmployeeMatch(SalesContext context, Employee employee)
        {
            var manager = context.Employees.Find(employee.ManagerId);

            if (manager != null && 
                manager.FirstName == employee.FirstName &&
                manager.LastName == employee.LastName &&
                manager.DOB == employee.DOB)
            {
                return "An employee cannot be their own manager.";
            }
            else
            {
                return "";
            }
        }

        public static string CheckSales(SalesContext context, Sales s1)
        {
            Sales? dbSale = context.Sales
                .FirstOrDefault(s => s.EmployeeId == s1.EmployeeId &&
                                     s.Quarter == s1.Quarter &&
                                     s.Year == s1.Year);

            if (dbSale == null)
            {
                return "";
            }
            else
            {
                var emp = context.Employees.Find(s1.EmployeeId);
                return ($"Sales for {emp.FullName} for Quarter {s1.Quarter}, {s1.Year} already exists in the database.");
            }
        }
    }
}
