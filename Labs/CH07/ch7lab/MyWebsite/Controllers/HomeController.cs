using Microsoft.AspNetCore.Mvc;

namespace MyWebsite.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Active"] = "Home";
            return View();
        }

        public IActionResult About()
        {
            ViewData["Active"] = "About";
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Active"] = "Contact";
            var contacts = new Dictionary<string, string>();
            contacts["Phone"] = "123-456-7890";
            contacts["Email"] = "me@mywebsite.com";
            contacts["Facebook"] = "facebook.com/mywebsite";
            return View(contacts);
        }
    }
}