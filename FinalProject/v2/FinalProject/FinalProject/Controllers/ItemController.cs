using FinalProject.Data;
using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    public class ItemController : Controller
    {
        private Repository<Item> items;
        public ItemController(ApplicationDbContext context)
        {
            items = new Repository<Item>(context);
        }

        public async Task<IActionResult> Index()
        {
            return View(await items.GetAllAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await items.GetByIdAsync(id, new QueryOptions<Item>() { Includes = "BuildItems.Build" }));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId, Name, Cost")] Item item)
        {
            if (ModelState.IsValid)
            {
                await items.AddAsync(item);
                return RedirectToAction("Index");
            }
            return View(item);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            return View(await items.GetByIdAsync(id, new QueryOptions<Item> { Includes = "BuildItems.Build" }));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Item item)
        {
            await items.DeleteAsync(item.ItemId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            return View(await items.GetByIdAsync(id, new QueryOptions<Item> { Includes = "BuildItems.Build" }));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Item item)
        {
            if (ModelState.IsValid)
            {
                await items.UpdateAsync(item);
                return RedirectToAction("Index");
            }
            return View(item);
        }
    }
}
