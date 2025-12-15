using FinalProject.Models;
using Microsoft.AspNetCore.Identity;

namespace FinalProject.ViewModels
{
    public class UserViewModel
    {
        public IEnumerable<ApplicationUser> Users { get; set; } = null!;

        public IEnumerable<IdentityRole> Roles { get; set; } = null!;
    }
}
