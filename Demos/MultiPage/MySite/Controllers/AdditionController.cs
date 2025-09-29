using Microsoft.AspNetCore.Mvc;
using MySite.Models;

namespace MySite.Controllers
{
    public class AdditionController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Message = "Welcome to the Addition page!";
            return View();
        }

        [HttpPost]
        public IActionResult Add(Addition a1)
        {
            a1.CalculateSum();
            return View(a1);
        }
    }
}
