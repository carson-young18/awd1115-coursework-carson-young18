using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Project_1.Models;

namespace Project_1.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            Quote quote = new Quote();
            return View(quote);
        }

        [HttpPost]
        public IActionResult Index(Quote quote)
        {
            if (ModelState.IsValid)
            {
                return View(quote);
            }
            return View(quote);
        }
    }
}