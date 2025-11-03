using Microsoft.AspNetCore.Mvc;
using QuarterlySalesApp.Models;
using QuarterlySalesApp.Models.Validation;

namespace QuarterlySalesApp.Controllers
{
    public class ValidationController : Controller
    {
        private SalesContext context { get; set; }

        public ValidationController(SalesContext ctx)
        {
            context = ctx;
        }

        public JsonResult CheckEmployee(DateTime dob, string firstName, string lastName)
        {
            Employee employee = new Employee
            {
                FirstName = firstName,
                LastName = lastName,
                DOB = dob
            };

            string msg = Validate.CheckEmployee(context, employee);

            if (string.IsNullOrEmpty(msg))
            {
                return Json(true);
            }
            else
            {
                return Json(msg);
            }
        }

        public JsonResult CheckManager(int managerId, string firstName, string lastName, DateTime dob)
        {
            var employee = new Employee
            {
                FirstName = firstName,
                LastName = lastName,
                DOB = dob,
                ManagerId = managerId
            };

            string msg = Validate.CheckManagerEmployeeMatch(context, employee);

            if (string.IsNullOrEmpty(msg))
            {
                return Json(true);
            }
            else
            {
                return Json(msg);
            }
        }

        public JsonResult CheckSales(int quarter, int year, int employeeId)
        {
            var sale = new Sales
            {
                Quarter = quarter,
                Year = year,
                EmployeeId = employeeId
            };
            string msg = Validate.CheckSales(context, sale);
            if (string.IsNullOrEmpty(msg))
            {
                return Json(true);
            }
            else
            {
                return Json(msg);
            }
        }
    }
}
