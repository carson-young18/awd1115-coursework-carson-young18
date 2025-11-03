using Microsoft.AspNetCore.Mvc;
using QuarterlySalesApp.Models;
using QuarterlySalesApp.Models.Validation;

namespace QuarterlySalesApp.Controllers
{
    public class EmployeeController : Controller
    {
        private SalesContext context { get; set; }
        public EmployeeController(SalesContext ctx)
        {
            context = ctx;
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Employees = context.Employees.OrderBy(e => e.FirstName).ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Add(Employee employee)
        {
            string msg = Validate.CheckEmployee(context, employee);
            if (!string.IsNullOrEmpty(msg))
            {
                ModelState.AddModelError(nameof(Employee.DOB), msg);
            }

            msg = Validate.CheckManagerEmployeeMatch(context, employee);
            if (!string.IsNullOrEmpty(msg))
            {
                ModelState.AddModelError(nameof(Employee.ManagerId), msg);
            }

            if (ModelState.IsValid)
            {
                context.Employees.Add(employee);
                context.SaveChanges();
                TempData["message"] = $"{employee.FullName} added";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Employees = context.Employees.OrderBy(e => e.FirstName).ToList();
                return View();
            }
        }
    }
}
