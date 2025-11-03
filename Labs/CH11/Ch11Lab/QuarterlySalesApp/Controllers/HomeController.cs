using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuarterlySalesApp.Models;

namespace QuarterlySalesApp.Controllers
{
    public class HomeController : Controller
    {
        private SalesContext context { get; set; }
        public HomeController(SalesContext ctx)
        {
            context = ctx;
        }

        [HttpGet]
        public IActionResult Index(int id)
        {
            IQueryable<Sales> sales = context.Sales
                .Include(s => s.Employee)
                .OrderByDescending(s => s.Year);

            if(id > 0)
            {
                sales = sales.Where(s => s.EmployeeId == id);
            }

            SalesListViewModel vm = new SalesListViewModel
            {
                Sales = sales.ToList(),
                Employees = context.Employees.OrderBy(e => e.FirstName).ToList(),
                EmployeeId = id
            };

            return View(vm);
        }

        [HttpPost]
        public RedirectToActionResult Index(Employee employee)
        {
            string id = (employee.EmployeeId > 0) ? employee.EmployeeId.ToString() : "";
            return RedirectToAction("Index", new { id });
        }
    }
}
