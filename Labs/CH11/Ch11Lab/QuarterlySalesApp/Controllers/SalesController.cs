using Microsoft.AspNetCore.Mvc;
using QuarterlySalesApp.Models;
using QuarterlySalesApp.Models.Validation;

namespace QuarterlySalesApp.Controllers
{
    public class SalesController : Controller
    {
        private SalesContext context;

        public SalesController(SalesContext ctx)
        {
            context = ctx;
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Employees = context.Employees.OrderBy(e => e.LastName).ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Add(Sales sale)
        {
            string msg = Validate.CheckSales(context, sale);
            if (!string.IsNullOrEmpty(msg))
            {
                ModelState.AddModelError(nameof(Sales.EmployeeId), msg);
            }

            if (ModelState.IsValid)
            {
                context.Sales.Add(sale);
                context.SaveChanges();
                TempData["message"] = $"Sales added";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Employees = context.Employees.OrderBy(e => e.LastName).ToList();
                return View();
            }
        }
    }
}
