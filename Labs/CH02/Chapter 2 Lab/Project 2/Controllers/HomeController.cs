using Microsoft.AspNetCore.Mvc;
using Project_2.Models;
using System.Diagnostics;

namespace Project_2.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            TipCalculator tipCalculator = new TipCalculator();
            return View(tipCalculator);
        }

        [HttpPost]
        public IActionResult Index(TipCalculator tipCalculator)
        {
            if (ModelState.IsValid)
            {
                return View(tipCalculator);
            }
            return View(tipCalculator);
        }
    }
}
