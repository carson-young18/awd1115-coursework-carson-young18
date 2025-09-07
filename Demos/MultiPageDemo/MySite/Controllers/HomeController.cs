using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MySite.Models;

namespace MySite.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
