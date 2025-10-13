using GenericStore.Data;
using GenericStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GenericStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/products")]
    public class ProductsController : Controller
    {
        private readonly AppDbContext _db;
        public ProductsController(AppDbContext db) => _db = db;

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            ViewData["Active"] = "Admin";
            var list = await _db.Products.Include(p => p.Category)
                                         .OrderBy(p => p.Name)
                                         .ToListAsync();
            return View(list);
        }

        [HttpGet("create/")]
        public IActionResult Create()
        {
            ViewData["Categories"] = new SelectList(_db.Categories, "CategoryId", "Name");
            return View();
        }

        [HttpPost("create/"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Categories"] = new SelectList(_db.Categories, "CategoryId", "Name", product.CategoryId);
                return View(product);
            }

            product.Slug = Product.GenerateSlug(product.Name);
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            TempData["Message"] = $"Product '{product.Name}' added.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("edit/{id:int}/{slug}/")]
        public async Task<IActionResult> Edit(int id, string slug)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null) return NotFound();

            ViewData["Categories"] = new SelectList(_db.Categories, "CategoryId", "Name", product.CategoryId);
            return View(product);
        }

        [HttpPost("edit/{id:int}/{slug}/"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string slug, Product product)
        {
            if (id != product.ProductId) return BadRequest();

            if (!ModelState.IsValid)
            {
                ViewData["Categories"] = new SelectList(_db.Categories, "CategoryId", "Name", product.CategoryId);
                return View(product);
            }

            product.Slug = Product.GenerateSlug(product.Name);
            _db.Products.Update(product);
            await _db.SaveChangesAsync();

            TempData["Message"] = $"Product '{product.Name}' updated.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("delete/{id:int}/{slug}/")]
        public async Task<IActionResult> Delete(int id, string slug)
        {
            var product = await _db.Products.Include(p => p.Category)
                                            .FirstOrDefaultAsync(p => p.ProductId == id);
            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost("delete/{id:int}/{slug}/"), ValidateAntiForgeryToken, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id, string slug)
        {
            var product = await _db.Products.FindAsync(id);
            if (product != null)
            {
                _db.Products.Remove(product);
                await _db.SaveChangesAsync();
                TempData["Message"] = $"Product '{product.Name}' deleted.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}