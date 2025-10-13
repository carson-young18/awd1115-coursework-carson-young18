using GenericStore.Data;
using GenericStore.Models;
using GenericStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GenericStore.Controllers
{
    public class CartController : Controller
    {
        private const string SessionCartKey = "Cart";
        private readonly AppDbContext _db;

        public CartController(AppDbContext db) => _db = db;

        private List<CartItem> GetCart()
        {
            var json = HttpContext.Session.GetString(SessionCartKey);
            if (string.IsNullOrEmpty(json)) return new List<CartItem>();
            return JsonConvert.DeserializeObject<List<CartItem>>(json) ?? new List<CartItem>();
        }

        private void SaveCart(List<CartItem> items)
        {
            HttpContext.Session.SetString(SessionCartKey, JsonConvert.SerializeObject(items));
        }

        public IActionResult Index()
        {
            ViewData["Active"] = "Cart";
            var items = GetCart();
            var vm = new CartViewModel { Items = items };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Add(int productId, int quantity = 1)
        {
            var product = await _db.Products.FindAsync(productId);
            if (product == null) return NotFound();

            var cart = GetCart();
            var existing = cart.FirstOrDefault(i => i.ProductId == productId);
            if (existing != null)
            {
                existing.Quantity += quantity;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = productId,
                    ProductName = product.Name,
                    UnitPrice = product.Price,
                    Quantity = quantity,
                    ImageFileName = product.ImageFileName,
                    Slug = product.Slug
                });
            }
            SaveCart(cart);
            TempData["CartMessage"] = $"{product.Name} added to cart.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Remove(int productId)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                cart.Remove(item);
                SaveCart(cart);
                TempData["CartMessage"] = $"{item.ProductName} removed from cart.";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Purchase(string customerName)
        {
            var cart = GetCart();
            if (!cart.Any()) return RedirectToAction("Index");

            var order = new Order
            {
                CustomerName = customerName,
                TotalPrice = cart.Sum(i => i.LineTotal),
                TotalQuantity = cart.Sum(i => i.Quantity),
                OrderDate = DateTime.UtcNow
            };

            foreach (var c in cart)
            {
                order.OrderItems.Add(new OrderItem
                {
                    ProductId = c.ProductId,
                    ProductName = c.ProductName,
                    Quantity = c.Quantity,
                    UnitPrice = c.UnitPrice
                });
            }

            _db.Orders.Add(order);
            await _db.SaveChangesAsync();

            HttpContext.Session.Remove(SessionCartKey);

            TempData["CartMessage"] = "Purchase completed. Thank you!";
            return RedirectToAction("Index");
        }
    }
}
