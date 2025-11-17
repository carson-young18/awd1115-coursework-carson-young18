using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TripLog2.Models.DataAccess;
using TripLog2.Models.DomainModels;
using TripLog2.Models.ViewModels;

namespace TripLog2.Controllers
{
    public class TripController : Controller
    {
        private Repository<Trip> tripData {  get; set; }
        private Repository<Destination> destinationData { get; set; }
        private Repository<Accomodation> accomodationData { get; set; }
        private Repository<Activity> activitiyData { get; set; }

        public TripController(AppDbContext context)
        {
            tripData = new Repository<Trip>(context);
            accomodationData = new Repository<Accomodation>(context);
            destinationData = new Repository<Destination>(context);
            activitiyData = new Repository<Activity>(context);
        }
        public IActionResult Add()
        {
            // PAGE 1
            var vm = new Page1
            {
                Destinations = destinationData.List(new QueryOptions<Destination>())
                    .Select(d => new SelectListItem { Value = d.DestinationId.ToString(), Text = d.Name }),
                Accomodations = accomodationData.List(new QueryOptions<Accomodation>())
                    .Select(a => new SelectListItem { Value = a.AccomodationId.ToString(), Text = a.Name })
            };

            return View("Page1", vm);
        }

        [HttpPost]
        public IActionResult Add(Page1 vm)
        {
            if (!ModelState.IsValid)
            {
                // repopulate dropdowns
                vm.Destinations = destinationData.List(new QueryOptions<Destination>())
                    .Select(d => new SelectListItem { Value = d.DestinationId.ToString(), Text = d.Name });

                vm.Accomodations = accomodationData.List(new QueryOptions<Accomodation>())
                    .Select(a => new SelectListItem { Value = a.AccomodationId.ToString(), Text = a.Name });

                return View("Page1", vm);
            }

            // pass info to second page
            var vm2 = new Page2
            {
                DestinationId = vm.DestinationId,
                AccomodationId = vm.AccomodationId,
                StartDate = vm.StartDate!.Value,
                EndDate = vm.EndDate!.Value,
                Activities = activitiyData.List(new QueryOptions<Activity>())
                    .Select(a => new SelectListItem { Value = a.ActivityId.ToString(), Text = a.Name })
            };

            return View("Page2", vm2);
        }

        [HttpPost]
        public IActionResult AddTripFinal(Page2 vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Activities = activitiyData.List(new QueryOptions<Activity>())
                    .Select(a => new SelectListItem { Value = a.ActivityId.ToString(), Text = a.Name });

                return View("Page2", vm);
            }

            // create the Trip
            var trip = new Trip
            {
                DestinationId = vm.DestinationId,
                StartDate = vm.StartDate,
                EndDate = vm.EndDate,
                Accomodation = accomodationData.Get(vm.AccomodationId)!,
                Activities = activitiyData.List(new QueryOptions<Activity>())
                    .Where(a => vm.SelectedActivityIds.Contains(a.ActivityId))
                    .ToList()
            };

            tripData.Insert(trip);
            tripData.Save();

            return RedirectToAction("Index", "Home");
        }
    }
}
