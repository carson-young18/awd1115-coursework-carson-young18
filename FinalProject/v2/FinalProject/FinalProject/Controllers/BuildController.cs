using FinalProject.Data;
using FinalProject.Models;
using FinalProject.ViewModels;
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
        private readonly RiotApiService _riotApi;

        public BuildController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RiotApiService riotApi)
        {
            _context = context;
            _userManager = userManager;
            _items = new Repository<Item>(context);
            _builds = new Repository<Build>(context);
            _categories = new Repository<Category>(context);
            _champions = new Repository<Champion>(context);
            _riotApi = riotApi;
        }

        [Authorize]
        public async Task<IActionResult> Index(int? SelectedCategoryId, int? SelectedChampionId)
        {
            const string CATEGORY_FILTER = "SelectedCategoryId";
            const string CHAMPION_FILTER = "SelectedChampionId";

            var patch = await _riotApi.GetPatch();
            ViewBag.Patch = patch;

            if (SelectedCategoryId.HasValue)
                HttpContext.Session.SetInt32(CATEGORY_FILTER, SelectedCategoryId.Value);
            if (SelectedChampionId.HasValue)
                HttpContext.Session.SetInt32(CHAMPION_FILTER, SelectedChampionId.Value);

            SelectedCategoryId ??= HttpContext.Session.GetInt32(CATEGORY_FILTER);
            SelectedChampionId ??= HttpContext.Session.GetInt32(CHAMPION_FILTER);

            var userId = _userManager.GetUserId(User);
            var userBuilds = await _builds.GetAllByIdAsync(userId, "UserId", new QueryOptions<Build>
            {
                Includes = "Champion,Category"
            });

            if (SelectedCategoryId.HasValue)
                userBuilds = userBuilds.Where(b => b.CategoryId == SelectedCategoryId.Value).ToList();

            if (SelectedChampionId.HasValue)
                userBuilds = userBuilds.Where(b => b.ChampionId == SelectedChampionId.Value).ToList();

            BuildViewModel model = new BuildViewModel
            {
                Builds = userBuilds.ToList(),
                Categories = (await _categories.GetAllAsync()).ToList(),
                Champions = (await _champions.GetAllAsync()).ToList(),
                SelectedCategoryId = SelectedCategoryId,
                SelectedChampionId = SelectedChampionId,
                Message = TempData["message"] as string
            };

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AddEdit(int id)
        {
            BuildViewModel model = new BuildViewModel
            {
                Items = (await _items.GetAllAsync()).ToList(),
                Categories = (await _categories.GetAllAsync()).ToList(),
                Champions = (await _champions.GetAllAsync()).ToList()
            };

            if (id == 0)
            {
                ViewBag.Operation = "Add";
                model.Build = new Build();
                return View(model);
            }
            else
            {
                model.Build = await _builds.GetByIdAsync(id, new QueryOptions<Build>
                {
                    Includes = "BuildItems.Item, Category, Champion"
                });

                model.ItemIds = model.Build.BuildItems?
                    .Select(bi => bi.ItemId)
                    .ToArray() ?? Array.Empty<int>();

                ViewBag.Operation = "Edit";
                return View(model);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddEdit(BuildViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Items = (await _items.GetAllAsync()).ToList();
                model.Categories = (await _categories.GetAllAsync()).ToList();
                model.Champions = (await _champions.GetAllAsync()).ToList();
                return View(model);
            }

            Build build = model.Build;

            if (build.BuildId == 0)
            {
                int totalPrice = 0;

                build.UserId = _userManager.GetUserId(User);
                build.BuildItems = new List<BuildItem>();

                foreach (int id in model.ItemIds)
                {
                    build.BuildItems.Add(new BuildItem { ItemId = id });

                    Item item = await _items.GetByIdAsync(id, new QueryOptions<Item> { });
                    totalPrice += item.Cost;
                }

                build.TotalCost = totalPrice;

                await _builds.AddAsync(build);
                TempData["message"] = "Build added successfully";
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

                    model.Items = (await _items.GetAllAsync()).ToList();
                    model.Categories = (await _categories.GetAllAsync()).ToList();
                    model.Champions = (await _champions.GetAllAsync()).ToList();
                    return View(model);
                }

                existingBuild.Name = build.Name;
                existingBuild.CategoryId = build.CategoryId;
                existingBuild.ChampionId = build.ChampionId;

                existingBuild.BuildItems?.Clear();

                int totalPrice = 0;
                foreach (int id in model.ItemIds)
                {
                    existingBuild.BuildItems.Add(new BuildItem { ItemId = id });
                    Item item = await _items.GetByIdAsync(id, new QueryOptions<Item> { });
                    totalPrice += item.Cost;
                }

                existingBuild.TotalCost = totalPrice;

                await _builds.UpdateAsync(existingBuild);

                TempData["message"] = "Build updated successfully";
                return RedirectToAction("Index");
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _builds.DeleteAsync(id);
                TempData["message"] = $"Build deleted successfully";
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Build not found");
                return RedirectToAction("Index");
            }
        }

        [Authorize]
        public IActionResult ClearFilters()
        {
            HttpContext.Session.Remove("SelectedCategoryId");
            HttpContext.Session.Remove("SelectedChampionId");

            return RedirectToAction("Index");
        }
    }
}
