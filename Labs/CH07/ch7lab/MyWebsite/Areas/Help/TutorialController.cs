using Microsoft.AspNetCore.Mvc;

namespace MyWebsite.Areas.Help.Controllers
{
    [Area("Help")]
    public class TutorialController : Controller
    {
        public IActionResult Index(string id = "Page1")
        {
            ViewData["Active"] = "Help";
            ViewData["HelpActive"] = "Tutorial";
            return View(id);
        }
    }
}
