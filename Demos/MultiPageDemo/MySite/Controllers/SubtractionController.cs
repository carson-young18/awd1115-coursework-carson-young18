using Microsoft.AspNetCore.Mvc;
using MySite.Models;

namespace MySite.Controllers
{
    public class SubtractionController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            Subtraction subtraction = new Subtraction();
            subtraction.Difference = 0;
            return View(subtraction);
        }

        [HttpPost]
        public IActionResult Index(Subtraction s1)
        {
            s1.CalculateDifference();
            return View(s1);
        }
    }
}
