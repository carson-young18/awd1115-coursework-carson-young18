using AppointmentScheduler.Data;
using AppointmentScheduler.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppointmentScheduler.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        public HomeController(AppDbContext db) => _db = db;

        public async Task<IActionResult> Index()
        {
            var list = await _db.Appointments.Include(a => a.Customer)
                                             .OrderBy(a => a.StartDateTime)
                                             .ToListAsync();
            return View(list);
        }

        public IActionResult CreateAppointment()
        {
            ViewData["Customers"] = _db.Customers.Select(c => new { c.CustomerId, c.Username }).ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointment(Appointment model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Customers"] = _db.Customers.Select(c => new { c.CustomerId, c.Username }).ToList();
                return View(model);
            }

            if (model.StartDateTime <= DateTime.Now)
            {
                ModelState.AddModelError(nameof(model.StartDateTime), "Appointment must be in the future.");
                ViewData["Customers"] = _db.Customers.Select(c => new { c.CustomerId, c.Username }).ToList();
                return View(model);
            }

            bool conflict = await _db.Appointments.AnyAsync(a => a.StartDateTime == model.StartDateTime);
            if (conflict)
            {
                ModelState.AddModelError(nameof(model.StartDateTime), "This time slot is already taken. Please choose another hour.");
                ViewData["Customers"] = _db.Customers.Select(c => new { c.CustomerId, c.Username }).ToList();
                return View(model);
            }

            _db.Appointments.Add(model);
            await _db.SaveChangesAsync();
            TempData["Message"] = "Appointment created successfully.";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreateCustomer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(Customer customer)
        {
            if (!ModelState.IsValid) return View(customer);

            _db.Customers.Add(customer);
            await _db.SaveChangesAsync();
            TempData["Message"] = "Customer created. You can now create appointments.";
            return RedirectToAction(nameof(Index));
        }
    }
}