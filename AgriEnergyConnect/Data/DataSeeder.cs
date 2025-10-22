using AgriEnergyConnect.Models;
using Microsoft.EntityFrameworkCore;

namespace AgriEnergyConnect.Data
{
    public static class DataSeeder
    {
        public static async Task SeedFarmersAndProductsAsync(ApplicationDbContext context)
        {
            // Apply pending migrations
            await context.Database.MigrateAsync();

            // Only seed if no farmers exist
            if (!context.Farmers.Any())
            {
                var farmers = new List<Farmer>
                {
                    new Farmer { Name = "John Mthembu", Email = "john.mthembu@farmers.co.za", Location = "KwaZulu-Natal" },
                    new Farmer { Name = "Thandi Nkosi", Email = "thandi.nkosi@farmers.co.za", Location = "Mpumalanga" },
                    new Farmer { Name = "Sipho Dlamini", Email = "sipho.dlamini@farmers.co.za", Location = "Free State" },
                    new Farmer { Name = "Agri Energy Supplies", Email = "supply@agrienergy.co.za", Location = "Head Office" } // internal supplier
                };

                await context.Farmers.AddRangeAsync(farmers);
                await context.SaveChangesAsync();
            }

            // Only seed if no products exist
            if (!context.Products.Any())
            {
                var john = await context.Farmers.FirstOrDefaultAsync(f => f.Name == "John Mthembu");
                var thandi = await context.Farmers.FirstOrDefaultAsync(f => f.Name == "Thandi Nkosi");
                var sipho = await context.Farmers.FirstOrDefaultAsync(f => f.Name == "Sipho Dlamini");
                var agri = await context.Farmers.FirstOrDefaultAsync(f => f.Name == "Agri Energy Supplies");

                var products = new List<Product>
                {
                    new Product { Name = "Organic Maize", Category = "Grain", ProductionDate = DateTime.Now.AddMonths(-2), FarmerId = john.Id },
                    new Product { Name = "Free-Range Eggs", Category = "Livestock", ProductionDate = DateTime.Now.AddMonths(-1), FarmerId = thandi.Id },
                    new Product { Name = "Fresh Spinach", Category = "Vegetables", ProductionDate = DateTime.Now.AddDays(-10), FarmerId = sipho.Id },
                    new Product { Name = "Solar Fertilizer Kit", Category = "Agri Supplies", ProductionDate = DateTime.Now, FarmerId = agri.Id }
                };

                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }
        }
    }
}
