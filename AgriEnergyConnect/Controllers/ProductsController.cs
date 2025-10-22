using AgriEnergyConnect.Data;
using AgriEnergyConnect.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgriEnergyConnect.Controllers
{
    [Authorize(Roles = "Farmer")] // Only farmers can manage products
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        [AllowAnonymous]
        public async Task<IActionResult> Index(string category, DateTime? startDate, DateTime? endDate)
        {
            var products = _context.Products.Include(p => p.Farmer).AsQueryable();

            if (!string.IsNullOrEmpty(category))
                products = products.Where(p => p.Category.Contains(category));

            if (startDate.HasValue && endDate.HasValue)
                products = products.Where(p => p.ProductionDate >= startDate && p.ProductionDate <= endDate);

            return View(await products.ToListAsync());
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewBag.Farmers = _context.Farmers.ToList();
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                TempData["ToastMessage"] = "Product added successfully!";
                TempData["ToastType"] = "success";

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Farmers = _context.Farmers.ToList();
            return View(product);
        }

        // GET: Products/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            ViewBag.Farmers = _context.Farmers.ToList();
            return View(product);
        }

        // POST: Products/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(product);
                await _context.SaveChangesAsync();

                TempData["ToastMessage"] = "Product updated successfully!";
                TempData["ToastType"] = "info";

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Farmers = _context.Farmers.ToList();
            return View(product);
        }

        // GET: Products/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products
                .Include(p => p.Farmer)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null) return NotFound();

            return View(product);
        }

        // POST: Products/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                TempData["ToastMessage"] = "Product deleted successfully!";
                TempData["ToastType"] = "danger";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
