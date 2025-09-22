using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesOrdersApp.Data;
using SalesOrdersApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SalesOrdersApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .OrderBy(p => p.ProductName)
                .ToListAsync();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> AddEdit(int? id)
        {
            PopulateCategoriesDropDown();
            if (!id.HasValue)
            {
                return View(new Product());
            }

            var product = await _context.Products.FindAsync(id.Value);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEdit(int? id, Product product)
        {
            PopulateCategoriesDropDown(product.CategoryID);

            if (!ModelState.IsValid)
                return View(product);

            if (!id.HasValue || id == 0)
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(List));
            }
            else
            {
                var existing = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.ProductID == id.Value);
                if (existing == null) return NotFound();

                product.ProductID = id.Value;
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(List));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.ProductID == id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(List));
        }

        private void PopulateCategoriesDropDown(object? selected = null)
        {
            ViewBag.Categories = new SelectList(_context.Categories.OrderBy(c => c.CategoryName), "CategoryID", "CategoryName", selected);
        }
    }
}
