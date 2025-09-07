using Microsoft.AspNetCore.Mvc;
using MySite.Models;


namespace MySite.Controllers
{
    public class AdditionController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Message = "Addition Page";
            Addition addition = new Addition();
            addition.Sum = 0;
            return View(addition);
        }

        [HttpPost]
        public IActionResult Index(Addition a1)
        {
            a1.CalculateSum();
            return View(a1);
        }
    }
}
