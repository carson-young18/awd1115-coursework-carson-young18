using Microsoft.AspNetCore.Mvc;
using hot1.Models;

namespace hot1.Controllers
{
    public class DistanceConverterController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new DistanceConverter());
        }

        [HttpPost]
        public IActionResult Index(DistanceConverter model)
        {
            if (ModelState.IsValid)
            {
                return View(model);
            }
            return View(new DistanceConverter());
        }
    }
}
