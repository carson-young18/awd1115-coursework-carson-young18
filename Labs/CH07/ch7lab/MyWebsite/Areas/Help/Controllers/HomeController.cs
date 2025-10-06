using Microsoft.AspNetCore.Mvc;

namespace MyWebsite.Areas.Help.Controllers
{
    [Area("Help")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["HelpActive"] = "Home";
            ViewData["Active"] = "Help";
            return View();
        }
    }
}