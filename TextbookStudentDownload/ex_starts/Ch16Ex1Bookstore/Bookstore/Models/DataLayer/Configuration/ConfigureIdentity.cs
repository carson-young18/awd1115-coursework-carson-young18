using Microsoft.AspNetCore.Identity;

namespace Bookstore.Models
{
    public class ConfigureIdentity
    {
        public static async Task CreateAdminUserAsync(IServiceProvider provider)
        {
            var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = provider.GetRequiredService<UserManager<User>>();

            string username = "Admin";
            string password = "Sesame";
            string roleName = "Admin";

            // If the Admin role doesn't exist, create it
            if (await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            // If the Admin user doesn't exist, create it and add it to the Admin role
            if(await userManager.FindByNameAsync(username)==null)
            {
                User user = new User {UserName = username};
                var result = await userManager.CreateAsync(user, password);
                if(result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
        }
    }

}
