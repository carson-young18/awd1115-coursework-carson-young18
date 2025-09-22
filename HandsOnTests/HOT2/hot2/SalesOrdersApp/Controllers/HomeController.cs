using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SalesOrdersApp.Models;

namespace SalesOrdersApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
