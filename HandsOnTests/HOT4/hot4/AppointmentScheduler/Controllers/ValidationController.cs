using AppointmentScheduler.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppointmentScheduler.Controllers
{
    public class ValidationController : Controller
    {
        private readonly AppDbContext _db;
        public ValidationController(AppDbContext db) => _db = db;

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> CheckStartDateTime(DateTime startDateTime, int? appointmentId)
        {
            if (startDateTime <= DateTime.Now)
            {
                return Json("The appointment must be scheduled for a future date/time.");
            }

            if (startDateTime.Minute != 0 || startDateTime.Second != 0)
            {
                return Json("Appointments must start exactly on the hour (minutes and seconds must be 00).");
            }

            bool exists = await _db.Appointments
                .AnyAsync(a => a.StartDateTime == startDateTime && a.AppointmentId != appointmentId);

            if (exists)
            {
                return Json("This appointment slot is already taken. Please choose another time.");
            }

            return Json(true);
        }
    }
}
