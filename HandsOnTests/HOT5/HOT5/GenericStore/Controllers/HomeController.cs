using GenericStore.Data;
using GenericStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GenericStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        public HomeController(AppDbContext db) => _db = db;

        public async Task<IActionResult> Index(int? categoryId)
        {
            ViewData["Active"] = "Home";
            var categories = await _db.Categories.OrderBy(c => c.Name).ToListAsync();
            ViewData["Categories"] = categories;

            var products = _db.Products.Include(p => p.Category).AsQueryable();

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                products = products.Where(p => p.CategoryId == categoryId.Value);
                ViewData["ActiveCategoryId"] = categoryId.Value;
            }

            return View(await products.OrderBy(p => p.Name).ToListAsync());
        }

        [HttpGet("product/{slug}/")]
        public async Task<IActionResult> Details(string slug)
        {
            var product = await _db.Products.Include(p => p.Category)
                                            .FirstOrDefaultAsync(p => p.Slug == slug);
            if (product == null) return NotFound();
            ViewData["Active"] = "Home";
            return View(product);
        }
    }
}