using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TripLog2.Models;
using TripLog2.Models.DataAccess;
using TripLog2.Models.DomainModels;

namespace TripLog2.Controllers
{
    public class HomeController : Controller
    {
        private Repository<Trip> data {  get; set; }

        public HomeController(AppDbContext context)
        {
            data = new Repository<Trip>(context);
        }

       public IActionResult Index()
        {
            var options = new QueryOptions<Trip>
            {
                Includes = "Destination, Accomodation, Activities",
                OrderBy = t => t.StartDate!
            };

            var trips = data.List(options);
            return View(trips);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var trip = data.Get(id);

            if (trip != null)
            {
                data.Delete(trip);
                data.Save();
            }

            return RedirectToAction("Index");
        }

    }
}
