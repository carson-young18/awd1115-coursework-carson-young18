using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TripPlannerApp.Data;
using TripPlannerApp.Models;
using TripPlannerApp.Models.ViewModels;

namespace TripPlannerApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var trips = _context.Trips.ToList();
            return View(trips);
        }

        [HttpGet]
        public IActionResult AddDestination()
        {
            ViewBag.SubHeader = "Add Trip Destination and Dates";
            return View();
        }

        [HttpPost]
        public IActionResult AddDestination(TripDestinationViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            TempData["Destination"] = model.Destination;
            TempData["Accommodation"] = model.Accommodation;
            TempData["StartDate"] = model.StartDate;
            TempData["EndDate"] = model.EndDate;

            return RedirectToAction("AddAccommodation");
        }

        [HttpGet]
        public IActionResult AddAccommodation()
        {
            ViewBag.SubHeader = $"Add Info for {TempData.Peek("Accommodation")?.ToString()}";
            return View();
        }

        [HttpPost]
        public IActionResult AddAccommodation(TripAccommodationViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            TempData["AccommodationPhone"] = model.AccommodationPhone;
            TempData["AccommodationEmail"] = model.AccommodationEmail;

            return RedirectToAction("AddActivities");
        }

        [HttpGet]
        public IActionResult AddActivities()
        {
            ViewBag.SubHeader = $"Add Things To Do in {TempData.Peek("Destination")?.ToString()}";
            return View();
        }

        [HttpPost]
        public IActionResult AddActivities(TripActivitiesViewModel model)
        {
            var trip = new Trip
            {
                Destination = TempData["Destination"]?.ToString() ?? "",
                StartDate = Convert.ToDateTime(TempData["StartDate"]),
                EndDate = Convert.ToDateTime(TempData["EndDate"]),
                Accommodation = TempData["Accommodation"]?.ToString() ?? "",
                AccommodationPhone = TempData["AccommodationPhone"]?.ToString(),
                AccommodationEmail = TempData["AccommodationEmail"]?.ToString(),
                Activity1 = model.Activity1,
                Activity2 = model.Activity2,
                Activity3 = model.Activity3
            };

            _context.Trips.Add(trip);
            _context.SaveChanges();

            TempData.Clear();
            TempData["Message"] = $"Trip to {trip.Destination} successfully!";
            return RedirectToAction("Index");
        }

        public IActionResult Cancel()
        {
            TempData.Clear();
            return RedirectToAction("Index");
        }
    }
}