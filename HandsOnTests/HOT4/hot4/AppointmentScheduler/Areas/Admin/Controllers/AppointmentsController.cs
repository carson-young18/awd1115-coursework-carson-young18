using AppointmentScheduler.Data;
using AppointmentScheduler.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AppointmentScheduler.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AppointmentsController : Controller
    {
        private readonly AppDbContext _db;

        public AppointmentsController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var appointments = await _db.Appointments
                .Include(a => a.Customer)
                .OrderBy(a => a.StartDateTime)
                .ToListAsync();

            return View(appointments);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var appointment = await _db.Appointments.FindAsync(id);
            if (appointment == null)
                return NotFound();

            ViewBag.CustomerList = new SelectList(_db.Customers, "CustomerId", "Username", appointment.CustomerId);
            return View(appointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CustomerList = new SelectList(_db.Customers, "CustomerId", "Username", appointment.CustomerId);
                return View(appointment);
            }

            _db.Update(appointment);
            await _db.SaveChangesAsync();
            TempData["Message"] = "Appointment updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var appointment = await _db.Appointments.FindAsync(id);
            if (appointment == null)
                return NotFound();

            _db.Appointments.Remove(appointment);
            await _db.SaveChangesAsync();

            TempData["Message"] = "Appointment deleted.";
            return RedirectToAction(nameof(Index));
        }
    }
}