using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace AgriEnergyConnect.Data
{
    public static class UserSeeder
    {
        public static async Task SeedUsersAsync(UserManager<IdentityUser> userManager)
        {
            var employee = await userManager.FindByEmailAsync("employee@agri.com");
            if (employee == null)
            {
                var user = new IdentityUser
                {
                    UserName = "employee@agri.com",
                    Email = "employee@agri.com",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, "Employee123!");
                await userManager.AddToRoleAsync(user, "Employee");
            }

            var farmer = await userManager.FindByEmailAsync("farmer@agri.com");
            if (farmer == null)
            {
                var user = new IdentityUser
                {
                    UserName = "farmer@agri.com",
                    Email = "farmer@agri.com",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, "Farmer123!");
                await userManager.AddToRoleAsync(user, "Farmer");
            }
        }
    }
}
