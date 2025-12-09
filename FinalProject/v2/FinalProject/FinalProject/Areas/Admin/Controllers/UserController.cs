using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using FinalProject.Models;

namespace FinalProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class UserController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            List<ApplicationUser> users = new List<ApplicationUser>();

            foreach (ApplicationUser user in _userManager.Users)
            {
                user.Roles = await _userManager.GetRolesAsync(user);
                users.Add(user);
            }

            UserViewModel model = new UserViewModel
            {
                Users = users,
                Roles = _roleManager.Roles
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdmin()
        {
            await _roleManager.CreateAsync(new IdentityRole("Admin"));
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            await _roleManager.DeleteAsync(role);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddAdmin(string id)
        {
            IdentityRole role = await _roleManager.FindByNameAsync("Admin");
            if (role == null)
            {
                TempData["message"] = "Admin role does not exist.";
            }
            else
            {
                ApplicationUser user = await _userManager.FindByIdAsync(id);
                await _userManager.AddToRoleAsync(user, role.Name);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveAdmin(string id)
        {

            ApplicationUser user = await _userManager.FindByIdAsync(id);
            await _userManager.RemoveFromRoleAsync(user, "Admin");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    string errorMessage = "";
                    foreach (IdentityError error in result.Errors)
                    {
                        errorMessage += error.Description + " ";
                    }
                    TempData["message"] = errorMessage;
                }
            }
            return RedirectToAction("Index");
        }
    }
}
