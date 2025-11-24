using FinalProject.Data;
using FinalProject.Models;
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
            return View(await champions.GetAllAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await champions.GetByIdAsync(id, new QueryOptions<Champion>() { Includes = "Builds" }));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChampionId, Name")] Champion champ)
        {
            if (ModelState.IsValid)
            {
                await champions.AddAsync(champ);
                return RedirectToAction("Index");
            }
            return View(champ);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            return View(await champions.GetByIdAsync(id, new QueryOptions<Champion> { Includes = "Builds" }));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Champion champ)
        {
            await champions.DeleteAsync(champ.ChampionId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            return View(await champions.GetByIdAsync(id, new QueryOptions<Champion> { Includes = "Builds" }));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Champion champ)
        {
            if (ModelState.IsValid)
            {
                await champions.UpdateAsync(champ);
                return RedirectToAction("Index");
            }
            return View(champ);
        }
    }
}
