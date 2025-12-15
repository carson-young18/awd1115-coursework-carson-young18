using FinalProject.Data;
using FinalProject.Models;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    public class ChampionController : Controller
    {
        private Repository<Champion> champions;
        public ChampionController(ApplicationDbContext context)
        {
            champions = new Repository<Champion>(context);
        }

        public async Task<IActionResult> Index()
        {
            ChampionViewModel model = new ChampionViewModel
            {
                Champions = await champions.GetAllAsync()
            };

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            ChampionViewModel model = new ChampionViewModel
            {
                Champion = await champions.GetByIdAsync(id, new QueryOptions<Champion> { Includes = "Builds" })
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ChampionViewModel model = new ChampionViewModel
            {
                Champion = new Champion()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ChampionViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool exists = (await champions.GetAllAsync())
                    .Any(c => c.Name.ToLower() == model.Champion.Name.ToLower());

                if (exists)
                {
                    ModelState.AddModelError("Champion.Name", "A champion with this name already exists.");
                }


                await champions.AddAsync(model.Champion);
                TempData["message"] = $"Champion {model.Champion.Name} was created successfully.";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            ChampionViewModel model = new ChampionViewModel
            {
                Champion = await champions.GetByIdAsync(id, new QueryOptions<Champion> { Includes = "Builds" })
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ChampionViewModel model)
        {
            await champions.DeleteAsync(model.Champion.ChampionId);
            TempData["message"] = $"Champion {model.Champion.Name} was deleted successfully.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ChampionViewModel model = new ChampionViewModel
            {
                Champion = await champions.GetByIdAsync(id, new QueryOptions<Champion> { Includes = "Builds" })
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ChampionViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool exists = (await champions.GetAllAsync())
                    .Any(c =>
                    c.Name.ToLower() == model.Champion.Name.ToLower() &&
                    c.ChampionId != model.Champion.ChampionId);

                if (exists)
                {
                    ModelState.AddModelError("Champion.Name", "A champion with this name already exists.");
                }


                Champion existingChampion = await champions.GetByIdAsync(model.Champion.ChampionId, new QueryOptions<Champion> { });

                existingChampion.Name = model.Champion.Name;

                await champions.UpdateAsync(existingChampion);
                TempData["message"] = $"Champion {model.Champion.Name} was updated successfully.";
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
