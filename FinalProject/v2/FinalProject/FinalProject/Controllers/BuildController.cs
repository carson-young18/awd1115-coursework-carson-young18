using FinalProject.Data;
using FinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    public class BuildController : Controller
    {
        private readonly ApplicationDbContext _context;
        private Repository<Item> _items;
        private Repository<Build> _builds;
        private Repository<Category> _categories;
        private Repository<Champion> _champions;
        private readonly UserManager<ApplicationUser> _userManager;

        public BuildController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _items = new Repository<Item>(context);
            _builds = new Repository<Build>(context);
            _categories = new Repository<Category>(context);
            _champions = new Repository<Champion>(context);
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var userBuilds = await _builds.GetAllByIdAsync(userId, "UserId", new QueryOptions<Build>
            {
                Includes = "Champion,Category"
            });

            return View(userBuilds);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AddEdit(int id)
        {
            ViewBag.Items = await _items.GetAllAsync();
            ViewBag.Categories = await _categories.GetAllAsync();
            ViewBag.Champions = await _champions.GetAllAsync();

            if (id == 0)
            {
                ViewBag.Operation = "Add";
                return View(new Build());
            }
            else
            {
                Build build = await _builds.GetByIdAsync(id, new QueryOptions<Build>
                {
                    Includes = "BuildItems.Item, Category, Champion"
                });
                ViewBag.Operation = "Edit";
                return View(build);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddEdit(Build build, int[] itemIds)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Items = await _items.GetAllAsync();
                ViewBag.Categories = await _categories.GetAllAsync();
                ViewBag.Champions = await _champions.GetAllAsync();
                return View(build);
            }

            if (build.BuildId == 0)
            {
                int totalPrice = 0;

                build.UserId = _userManager.GetUserId(User);

                foreach (int id in itemIds)
                {
                    build.BuildItems ??= new List<BuildItem>();
                    build.BuildItems.Add(new BuildItem { ItemId = id });

                    Item item = await _items.GetByIdAsync(id, new QueryOptions<Item> { });
                    totalPrice += item.Cost;
                }

                build.TotalCost = totalPrice;

                await _builds.AddAsync(build);
                return RedirectToAction("Index", "Build");
            }
            else
            {
                var existingBuild = await _builds.GetByIdAsync(
                    build.BuildId,
                    new QueryOptions<Build> { Includes = "BuildItems" }
                );

                if (existingBuild == null)
                {
                    ModelState.AddModelError("", "Build not found");

                    ViewBag.Items = await _items.GetAllAsync();
                    ViewBag.Categories = await _categories.GetAllAsync();
                    ViewBag.Champions = await _champions.GetAllAsync();
                    return View(build);
                }

                existingBuild.Name = build.Name;
                existingBuild.CategoryId = build.CategoryId;
                existingBuild.ChampionId = build.ChampionId;

                existingBuild.BuildItems?.Clear();

                int totalPrice = 0;
                foreach (int id in itemIds)
                {
                    existingBuild.BuildItems.Add(new BuildItem { ItemId = id });
                    Item item = await _items.GetByIdAsync(id, new QueryOptions<Item> { });
                    totalPrice += item.Cost;
                }

                existingBuild.TotalCost = totalPrice;

                try
                {
                    await _builds.UpdateAsync(existingBuild);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error: {ex.GetBaseException().Message}");

                    ViewBag.Items = await _items.GetAllAsync();
                    ViewBag.Categories = await _categories.GetAllAsync();
                    ViewBag.Champions = await _champions.GetAllAsync();
                    return View(build);
                }

                return RedirectToAction("Index", "Build");
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _builds.DeleteAsync(id);
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Build not found");
                return RedirectToAction("Index");
            }
        }
    }
}
