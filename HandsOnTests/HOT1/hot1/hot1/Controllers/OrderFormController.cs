using Microsoft.AspNetCore.Mvc;
using hot1.Models;

namespace hot1.Controllers
{
    public class OrderFormController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new OrderForm());
        }

        [HttpPost]
        public IActionResult Index(OrderForm model)
        {
            if (ModelState.IsValid)
            {
                model.ApplyDiscount();
                return View(model);
            }
            return View(new OrderForm());
        }
    }
}
