using FinalProject.Data;
using FinalProject.Models;
using FinalProject.ViewModels;
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
            ItemViewModel model = new ItemViewModel
            {
                Items = await items.GetAllAsync(),
                Message = TempData["message"] as string
            };

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            ItemViewModel model = new ItemViewModel
            {
                Item = await items.GetByIdAsync(id, new QueryOptions<Item> { Includes = "BuildItems.Build" }),
                Message = TempData["message"] as string
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ItemViewModel model = new ItemViewModel
            {
                Item = new Item()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool exists = (await items.GetAllAsync())
                    .Any(i => i.Name.ToLower() == model.Item.Name.ToLower());

                if (exists)
                {
                    ModelState.AddModelError("Item.Name", "An item with this name already exists.");
                }


                await items.AddAsync(model.Item);
                TempData["message"] = $"Item '{model.Item.Name}' was created successfully.";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            ItemViewModel model = new ItemViewModel
            {
                Item = await items.GetByIdAsync(id, new QueryOptions<Item> { Includes = "BuildItems.Build" })
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ItemViewModel model)
        {
            await items.DeleteAsync(model.Item.ItemId);
            TempData["message"] = $"Item '{model.Item.Name}' was deleted successfully.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ItemViewModel model = new ItemViewModel
            {
                Item = await items.GetByIdAsync(id, new QueryOptions<Item> { Includes = "BuildItems.Build" })
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool exists = (await items.GetAllAsync())
                    .Any(i => i.Name.ToLower() == model.Item.Name.ToLower()
                        && i.ItemId != model.Item.ItemId);

                if (exists)
                {
                    ModelState.AddModelError("Item.Name", "An item with this name already exists.");
                }


                Item existingItem = await items.GetByIdAsync(model.Item.ItemId, new QueryOptions<Item> { });

                existingItem.Name = model.Item.Name;
                existingItem.Cost = model.Item.Cost;

                await items.UpdateAsync(existingItem);
                TempData["message"] = $"Item '{model.Item.Name}' was updated successfully.";
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
